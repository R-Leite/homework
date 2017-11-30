package controllers
import play.api._
import play.api.mvc._
import play.api.data.Form
import play.api.data.Forms._
import models._
import play.api.Play.current
import play.api.i18n.Messages.Implicits._

import anorm._
import play.api.db.DB

import play.api.libs.json.{JsObject, _}
import play.api.libs.json.Json

//ユーザー基本情報定義

class LockController extends Controller with Secured {

  val dFrom = Form (
    mapping (
      "display-date" -> text
    )(dateForm.apply)(dateForm.unapply)
  )

//  val DateInfoForm = Form (
//    mapping (
//    "datepickerFrom"  -> optional(text),
//    "datepickerTo"  -> optional(text)
//    )(OverTimeSummaryInfo.apply)(OverTimeSummaryInfo.unapply)
//  )
//
//  def lock(from:String, to:String) = Action {
//    val today = CalendarDates.GetToday
//    var datefrom =  from + "-01"
//    var dateto =  to + "-31"
//    if(datefrom > today) datefrom = today
//    if(dateto   > today) dateto   = today
// 
//    val list = Aggregate.getAllOverTimeList(datefrom, dateto)
//    val json = Json.toJson(list)
//    Ok(json)
//  }

  def lockAttendance(empCode: String, from: String, to: String) = withUser { user => implicit request =>
    val jsonObj = Json.stringify(request.body.asJson.get)
    val jsonData = Json.parse(jsonObj)

    println("hoge")
    Ok("success")
//    NewAttandance.lockAttendance()
  }

//  def lockHoliday = Action {
//  }
//
//  def unlock(from:String, to:String) = Action {
//    val today = CalendarDates.GetToday
//    var datefrom =  from + "-01"
//    var dateto =  to + "-31"
//    if(datefrom > today) datefrom = today
//    if(dateto   > today) dateto   = today
// 
//    val list = Aggregate.getExchangeHolidayList(datefrom, dateto)
//    val json = Json.toJson(list)
//    Ok(json)
//  }
//
//  def unlockAttendance = Action {
//  }
//
//  def unlockHoliday = Action {
//  }
//
  // 表示
  def testView(symbol: String, year: String, month: String) = withUser { user => implicit request =>
    var disp_year = year
    var disp_month = month

    //URL等に直接入力された時用にチェック処理
    if(Validate.ValidateYear(year) == false || Validate.ValidateMonth(month) == false){
      disp_year = CalendarDates.GetNowYear
      disp_month = CalendarDates.GetNowMonth
    }
    Ok(views.html.lock_view(user.id, symbol, disp_year, disp_month))
  }

  // 月変更後表示
  def redirectShow = withUser { user => implicit request =>
    val display_date = dFrom.bindFromRequest.get.display_date
    val years = display_date.split('-').toList
    val year = years(0)
    val month = years(1)

    // バージョン情報
    val symbol = Play.application.configuration.getString("version.symbol").get
    Redirect(routes.LockController.testView(symbol, year, month))
  }

//  //表示
//  def show = withUser { user => implicit request =>      
//    val msg = request.flash.get("msg").getOrElse("")
//    val date_info = DateInfoForm.bindFromRequest.get
//
//    if(RoleAllocations.checkRollTypeTeamReader(user.id) || RoleAllocations.checkRollTypeAdmin(user.id)) {
//      if(date_info.datepickerFrom == None) {
//          val year = Utility.getNowFiscalYear()
//          val strPeriod  = Utility.convStartPeriod(year)
//          val endPeriod  = Utility.convEndPeriod(year)
//          Ok(views.html.manager.overTimeSummary(user.id, msg, strPeriod, endPeriod)).withSession(Security.username -> user.id, "timestamp" -> Utility.getTimeStamp )
//      } else {
//          Ok(views.html.manager.overTimeSummary(user.id, msg, date_info.datepickerFrom.get, date_info.datepickerTo.get)).withSession(Security.username -> user.id, "timestamp" -> Utility.getTimeStamp )
//      }
//    }else{
//      Ok(views.html.failure(user.id,"警告 : 管理者権限がありません")).withSession(Security.username -> user.id, "timestamp" -> Utility.getTimeStamp )
//    }
//  }
}

package controllers

import play.api._
import play.api.mvc._
import play.api.data.Form
import play.api.data.Forms._
import models._
import play.api.Play.current
import play.api.i18n.Messages.Implicits._
import play.api.db.DB

import play.api.libs.json.{JsObject, _}
import play.api.libs.json.Json

case class dateForm(display_date: String)
case class userForm(select_user: String)

class TimeCardController extends Controller with Secured
{
    val dForm = Form (
      mapping (
      "display-date" -> text
      )(dateForm.apply)(dateForm.unapply)
    )
    val uForm = Form (
      mapping (
      "select_user" -> text
      )(userForm.apply)(userForm.unapply)
    )

    // タイムカード編集画面表示
    def edit(symbol: String, employee_number: String, year: String, month: String) = withUser { user => implicit request =>
        var employee_id = 0
        var exist = true
        var disp_year = year
        var disp_month = month
        //URL等に直接入力された時用にチェック処理
        if(Validate.ValidateYear(year) == false || Validate.ValidateMonth(month) == false){
            disp_year = CalendarDates.GetNowYear
            disp_month = CalendarDates.GetNowMonth
        }
        try{
            //URLの従業員番号から従業員IDを取得
            employee_id = Employees.takeEmployeeID(employee_number)
        }
        catch{
            case e: Exception =>
            //存在しないユーザーの場合はセッションから取得
            employee_id = Employees.GetEmployeeId(user.id)
            exist = false
        }
        //現在閲覧しているデータのsMAccountName
        val employee_name = User.getUserName(employee_id).getOrElse(user.id)
        //閲覧権限の確認
        if(!RoleControl.getAttendance(user.id,employee_name) || !exist) {
            if(exist){
                Ok(views.html.failure(user.id,"警告 : 閲覧権限がありません")).withSession(Security.username -> user.id, "timestamp" -> Utility.getTimeStamp)
            }
            else {
                Ok(views.html.failure(user.id,"警告 : 対象のユーザーは存在しません")).withSession(Security.username -> user.id, "timestamp" -> Utility.getTimeStamp)
            }
        }
        else{
            Ok(views.html.timecard.timecardEdit(user.id, year+"-"+month, employee_number)).withSession(Security.username -> user.id, "timestamp" -> Utility.getTimeStamp)
        }
    }
    
    // 表示日付及び閲覧ユーザーの変更
    def redirectEdit = withUser { user => implicit request =>
        val display_date = dForm.bindFromRequest.get.display_date
        val years = display_date.split('-').toList
        val year  = years(0)
        val month = years(1)
        val employee_number = uForm.bindFromRequest.get.select_user
        //バージョン情報
        val symbol = Play.application.configuration.getString("version.symbol").get
        Redirect(routes.TimeCardController.edit(symbol, employee_number, year, month))
    }

    // タイムカード一覧表示
    def show(symbol: String, year: String, month: String) = withUser { user => implicit request =>
        var disp_year = year
        var disp_month = month
        //URL等に直接入力された時用にチェック処理
        if(Validate.ValidateYear(year) == false || Validate.ValidateMonth(month) == false){
            disp_year = CalendarDates.GetNowYear
            disp_month = CalendarDates.GetNowMonth
        }
        Ok(views.html.timecard.timecardShow(user.id, disp_year, disp_month))
    }

    // 表示日付変更
    def redirectShow = withUser { user => implicit request =>
        val display_date = dForm.bindFromRequest.get.display_date
        val years = display_date.split('-').toList
        val year  = years(0)
        val month = years(1)
        //バージョン情報
        val symbol = Play.application.configuration.getString("version.symbol").get
        Redirect(routes.TimeCardController.show(symbol, year, month))
    }

    //タイムカードの情報をjsonで取得
    def getTimeCard(date: String) = withUser { user => implicit request =>
        
        val employee_id = Employees.GetEmployeeId(user.id)
        val strPeriod = date + "/01"
        val endPeriod = date + "/31"
        val time_card_info = TimeCard.getList(employee_id, strPeriod, endPeriod)
        val sendJson = Json.toJson(time_card_info)

        Ok(sendJson)
    }

    //タイムカードの情報をjsonで取得
    def getList = withUser { user => implicit request =>
        //jsonデータの取得
        val jsonObj   = Json.stringify(request.body.asJson.get)
        val jsonData  = Json.parse(jsonObj)
        
        val employee_id = Employees.GetEmployeeId(user.id)
        val strPeriod = (jsonData \ "date").as[String] + "-01"
        val endPeriod = (jsonData \ "date").as[String] + "-31"
         
        val time_card_info = TimeCard.getList(employee_id, strPeriod, endPeriod)
        val sendJson = Json.toJson(time_card_info)

        Ok(sendJson)
    }

    //タイムカードの情報をjsonで取得
    def getDesignatedList(empCode: String) = withUser { user => implicit request =>
        //jsonデータの取得
        val jsonObj   = Json.stringify(request.body.asJson.get)
        val jsonData  = Json.parse(jsonObj)
        
        val employee_id = Employees.takeEmployeeID(empCode)
        val strPeriod = (jsonData \ "date").as[String] + "-01"
        val endPeriod = (jsonData \ "date").as[String] + "-31"
         
        val time_card_info = TimeCard.getList(employee_id, strPeriod, endPeriod)
        val sendJson = Json.toJson(time_card_info)

        Ok(sendJson)
    }

    //出社時刻の打刻
    def setArrival = withUser { user => implicit request => 
    
        //jsonデータの取得
        val jsonObj   = Json.stringify(request.body.asJson.get)
        val jsonData  = Json.parse(jsonObj)
        
        //必要なデータを取得
        val date = (jsonData \ "date").as[String]
        val arrival = (jsonData \ "arrival").as[String]
        val employee_id = Employees.GetEmployeeId(user.id) 
        val edit_by     = Employees.GetEmployeeId(user.id)
        val calendar_date_id = CalendarDates.getCalenderDataId(date)
        
        //タイムカードデータの存在チェック
        if(!TimeCard.exist(employee_id, calendar_date_id)){
            //存在しない場合は新規作成
            TimeCard.create(calendar_date_id, employee_id, edit_by, "")
        }
        
        //更新処理
        val info = TimeCard.getInfo(calendar_date_id, employee_id)
        val id = info.id.get
        val arrival_time = date + " " + arrival + ":00"
        val leave_time = info.leave_time.get
        val latest_arrival_time = arrival_time
        val latest_leave_time = info.t_leave.get
        val note_arrival = info.note_arrival.get
        val note_leave = info.note_leave.get
        val last_edit_by_arrival = CalendarDates.GetTime
        val last_edit_by_leave = info.last_edit_by_leave.get

        TimeCard.update(id, employee_id, arrival_time, leave_time, latest_arrival_time, latest_leave_time, edit_by, edit_by, note_arrival, note_leave, last_edit_by_arrival, last_edit_by_leave)

        Ok("success")
    }

    //退社時刻の打刻
    def setLeave = withUser { user => implicit request => 
        
        //jsonデータの取得
        val jsonObj   = Json.stringify(request.body.asJson.get)
        val jsonData  = Json.parse(jsonObj)
        
        //必要なデータを取得
        val date = (jsonData \ "date").as[String]
        val leave = (jsonData \ "leave").as[String]
        val employee_id = Employees.GetEmployeeId(user.id) 
        val edit_by     = Employees.GetEmployeeId(user.id)
        val calendar_date_id = CalendarDates.getCalenderDataId(date)
        
        //タイムカードデータの存在チェック
        if(!TimeCard.exist(employee_id, calendar_date_id)){
            //存在しない場合は新規作成
            TimeCard.create(calendar_date_id, employee_id, edit_by, "")
        }

        //更新処理
        val info = TimeCard.getInfo(calendar_date_id, employee_id)
        val id = info.id.get
        val arrival_time = info.arrival_time.get
        val leave_time = date + " " + leave + ":00"
        val latest_arrival_time = info.t_arrival.get
        val latest_leave_time = leave_time
        val note_arrival = info.note_arrival.get
        val note_leave = info.note_leave.get
        val last_edit_by_arrival = info.last_edit_by_arrival.get
        val last_edit_by_leave = CalendarDates.GetTime

        TimeCard.update(id, employee_id, arrival_time, leave_time, latest_arrival_time, latest_leave_time, edit_by, edit_by, note_arrival, note_leave, last_edit_by_arrival, last_edit_by_leave)

        Ok("success")
    }

    // 出社時刻の更新
    def updateArrival(employee_number: String) = withUser { user => implicit request =>
        
        // jsonデータの取得
        val jsonObj   = Json.stringify(request.body.asJson.get)
        val jsonData  = Json.parse(jsonObj)

        // 必要なデータを取得
        val date = (jsonData \ "date").as[String]
        val latest_arrival_time = (jsonData \ "latest_arrival_time").as[String]
        val note_arrival = (jsonData \ "note_arrival").as[String]
        val employee_id = Employees.takeEmployeeID(employee_number) 
        val edit_by_arrival = Employees.GetEmployeeId(user.id)
        val calendar_date_id = CalendarDates.getCalenderDataId(date)

        // タイムカードデータの存在チェック
        if(!TimeCard.exist(employee_id, calendar_date_id)){
            //存在しない場合は新規作成
            TimeCard.create(calendar_date_id, employee_id, edit_by_arrival, "")
        }

        val info = TimeCard.getInfo(calendar_date_id, employee_id)
        val id = info.id.get
        val arrival_time = info.arrival_time.get
        val leave_time   = info.leave_time.get
        val latest_leave_time = info.t_leave.get
        val edit_by_leave = info.edit_by_leave.get
        val note_leave = info.note_leave.get
        val last_edit_by_arrival = CalendarDates.GetTime
        val last_edit_by_leave = info.last_edit_by_leave.get

        // 更新処理
        TimeCard.update(id, employee_id, arrival_time, leave_time, latest_arrival_time, latest_leave_time, edit_by_arrival, edit_by_leave, note_arrival, note_leave, last_edit_by_arrival, last_edit_by_leave)

        Ok("success")
    }

    // 退社時刻の更新
    def updateLeave(employee_number: String) = withUser { user => implicit request =>
        
        // jsonデータの取得
        val jsonObj   = Json.stringify(request.body.asJson.get)
        val jsonData  = Json.parse(jsonObj)

        // 必要なデータを取得
        val date = (jsonData \ "date").as[String]
        val latest_leave_time = (jsonData \ "latest_leave_time").as[String]
        val note_leave = (jsonData \ "note_leave").as[String]
        val employee_id = Employees.takeEmployeeID(employee_number) 
        val edit_by_leave = Employees.GetEmployeeId(user.id)
        val calendar_date_id = CalendarDates.getCalenderDataId(date)

        // タイムカードデータの存在チェック
        if(!TimeCard.exist(employee_id, calendar_date_id)){
            //存在しない場合は新規作成
            TimeCard.create(calendar_date_id, employee_id, edit_by_leave, "")
        }

        val info = TimeCard.getInfo(calendar_date_id, employee_id)
        val id = info.id.get
        val arrival_time = info.arrival_time.get
        val leave_time   = info.leave_time.get
        val latest_arrival_time = info.t_arrival.get
        val edit_by_arrival = info.edit_by_arrival.get
        val note_arrival = info.note_arrival.get
        val last_edit_by_arrival = info.last_edit_by_arrival.get
        val last_edit_by_leave = CalendarDates.GetTime
        
        // 更新処理
        TimeCard.update(id, employee_id, arrival_time, leave_time, latest_arrival_time, latest_leave_time, edit_by_arrival, edit_by_leave, note_arrival, note_leave, last_edit_by_arrival, last_edit_by_leave)

        Ok("success")
    }

    //特定日付の総合情報を取得
    def getInfoByDate(date: String) = withUser { user => implicit request =>
        val info_by_date = TimeCard.getInfoByDate(date)
        val sendJson = Json.toJson(info_by_date)
        Ok(sendJson)
    }

    //特定月の総合情報を取得
    def getGeneralInfo(year: String, month: String) = withUser { user => implicit request =>
        val start = year + "-" + month + "-01"
        val end   = year + "-" + month + "-31"
        val info  = ManageUser.getUserList
        .map( (userData) => 
            generalInfo(
                userData.user_id,
                userData.employee_id,
                userData.employee_code,
                userData.employee_name,
                userData.internal_division_id,
                userData.is_left,
                TimeCard.getMonthInfo(userData.employee_id, start, end)
            )
        )
        val sendJson = Json.toJson(info)
        Ok(sendJson)
    }

    def viewTemplate = withUser { user => implicit request =>
        Ok(views.html.template.view_template(user.id))
    }
}

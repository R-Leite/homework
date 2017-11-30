package models
import anorm.SqlParser._
import anorm._
import play.api.db.DB
import play.api.Play.current
import java.util.Calendar

/**
  * Created by uga-yu on 2016/10/13.
  */

case class New_Attendance_Data (
  id:                  Int,
  attendance_date:     String,
  date_type_id:        Int,
  arrived_at:          String,
  left_at:             String
)

case class AllAttendanceData (
  id: Int,
  attendance_date: String,
  date_type_id:    Int,
  arrived_at:      String,
  left_at:         String,
  detail_id:       Option[Int],
  attendance_id:   Option[Int],
  order_id:        Option[Int],
  work_type_id:    Option[Int],
  hours:           Option[String],
  note:            Option[String],
  row_number:      Int,
  holiday_type_id: Int
)

object NewAttendance {
  
   val from_time_options = Seq(
        "00:00:59" -> "",
        "00:00:00" -> "00:00", "00:15:00" -> "00:15", "00:30:00" -> "00:30", "00:45:00" -> "00:45",
        "01:00:00" -> "01:00", "01:15:00" -> "01:15", "01:30:00" -> "01:30", "01:45:00" -> "01:45",
        "02:00:00" -> "02:00", "02:15:00" -> "02:15", "02:30:00" -> "02:30", "02:45:00" -> "02:45",
        "03:00:00" -> "03:00", "03:15:00" -> "03:15", "03:30:00" -> "03:30", "03:45:00" -> "03:45",
        "04:00:00" -> "04:00", "04:15:00" -> "04:15", "04:30:00" -> "04:30", "04:45:00" -> "04:45",
        "05:00:00" -> "05:00", "05:15:00" -> "05:15", "05:30:00" -> "05:30", "05:45:00" -> "05:45",
        "06:00:00" -> "06:00", "06:15:00" -> "06:15", "06:30:00" -> "06:30", "06:45:00" -> "06:45",
        "07:00:00" -> "07:00", "07:15:00" -> "07:15", "07:30:00" -> "07:30", "07:45:00" -> "07:45",
        "08:00:00" -> "08:00", "08:15:00" -> "08:15", "08:30:00" -> "08:30", "08:45:00" -> "08:45",
        "09:00:00" -> "09:00", "09:15:00" -> "09:15", "09:30:00" -> "09:30", "09:45:00" -> "09:45",
        "10:00:00" -> "10:00", "10:15:00" -> "10:15", "10:30:00" -> "10:30", "10:45:00" -> "10:45",
        "11:00:00" -> "11:00", "11:15:00" -> "11:15", "11:30:00" -> "11:30", "11:45:00" -> "11:45",
        "12:00:00" -> "12:00", "12:15:00" -> "12:15", "12:30:00" -> "12:30", "12:45:00" -> "12:45",
        "13:00:00" -> "13:00", "13:15:00" -> "13:15", "13:30:00" -> "13:30", "13:45:00" -> "13:45",
        "14:00:00" -> "14:00", "14:15:00" -> "14:15", "14:30:00" -> "14:30", "14:45:00" -> "14:45",
        "15:00:00" -> "15:00", "15:15:00" -> "15:15", "15:30:00" -> "15:30", "15:45:00" -> "15:45",
        "16:00:00" -> "16:00", "16:15:00" -> "16:15", "16:30:00" -> "16:30", "16:45:00" -> "16:45",
        "17:00:00" -> "17:00", "17:15:00" -> "17:15", "17:30:00" -> "17:30", "17:45:00" -> "17:45",
        "18:00:00" -> "18:00", "18:15:00" -> "18:15", "18:30:00" -> "18:30", "18:45:00" -> "18:45",
        "19:00:00" -> "19:00", "19:15:00" -> "19:15", "19:30:00" -> "19:30", "19:45:00" -> "19:45",
        "20:00:00" -> "20:00", "20:15:00" -> "20:15", "20:30:00" -> "20:30", "20:45:00" -> "20:45",
        "21:00:00" -> "21:00", "21:15:00" -> "21:15", "21:30:00" -> "21:30", "21:45:00" -> "21:45",
        "22:00:00" -> "22:00", "22:15:00" -> "22:15", "22:30:00" -> "22:30", "22:45:00" -> "22:45",
        "23:00:00" -> "23:00", "23:15:00" -> "23:15", "23:30:00" -> "23:30", "23:45:00" -> "23:45"
    )
  val to_time_options = Seq(
        "00:00:59" -> "",
        "00:00:00" -> "00:00", "00:15:00" -> "00:15", "00:30:00" -> "00:30", "00:45:00" -> "00:45",
        "01:00:00" -> "01:00", "01:15:00" -> "01:15", "01:30:00" -> "01:30", "01:45:00" -> "01:45",
        "02:00:00" -> "02:00", "02:15:00" -> "02:15", "02:30:00" -> "02:30", "02:45:00" -> "02:45",
        "03:00:00" -> "03:00", "03:15:00" -> "03:15", "03:30:00" -> "03:30", "03:45:00" -> "03:45",
        "04:00:00" -> "04:00", "04:15:00" -> "04:15", "04:30:00" -> "04:30", "04:45:00" -> "04:45",
        "05:00:00" -> "05:00", "05:15:00" -> "05:15", "05:30:00" -> "05:30", "05:45:00" -> "05:45",
        "06:00:00" -> "06:00", "06:15:00" -> "06:15", "06:30:00" -> "06:30", "06:45:00" -> "06:45",
        "07:00:00" -> "07:00", "07:15:00" -> "07:15", "07:30:00" -> "07:30", "07:45:00" -> "07:45",
        "08:00:00" -> "08:00", "08:15:00" -> "08:15", "08:30:00" -> "08:30", "08:45:00" -> "08:45",
        "09:00:00" -> "09:00", "09:15:00" -> "09:15", "09:30:00" -> "09:30", "09:45:00" -> "09:45",
        "10:00:00" -> "10:00", "10:15:00" -> "10:15", "10:30:00" -> "10:30", "10:45:00" -> "10:45",
        "11:00:00" -> "11:00", "11:15:00" -> "11:15", "11:30:00" -> "11:30", "11:45:00" -> "11:45",
        "12:00:00" -> "12:00", "12:15:00" -> "12:15", "12:30:00" -> "12:30", "12:45:00" -> "12:45",
        "13:00:00" -> "13:00", "13:15:00" -> "13:15", "13:30:00" -> "13:30", "13:45:00" -> "13:45",
        "14:00:00" -> "14:00", "14:15:00" -> "14:15", "14:30:00" -> "14:30", "14:45:00" -> "14:45",
        "15:00:00" -> "15:00", "15:15:00" -> "15:15", "15:30:00" -> "15:30", "15:45:00" -> "15:45",
        "16:00:00" -> "16:00", "16:15:00" -> "16:15", "16:30:00" -> "16:30", "16:45:00" -> "16:45",
        "17:00:00" -> "17:00", "17:15:00" -> "17:15", "17:30:00" -> "17:30", "17:45:00" -> "17:45",
        "18:00:00" -> "18:00", "18:15:00" -> "18:15", "18:30:00" -> "18:30", "18:45:00" -> "18:45",
        "19:00:00" -> "19:00", "19:15:00" -> "19:15", "19:30:00" -> "19:30", "19:45:00" -> "19:45",
        "20:00:00" -> "20:00", "20:15:00" -> "20:15", "20:30:00" -> "20:30", "20:45:00" -> "20:45",
        "21:00:00" -> "21:00", "21:15:00" -> "21:15", "21:30:00" -> "21:30", "21:45:00" -> "21:45",
        "22:00:00" -> "22:00", "22:15:00" -> "22:15", "22:30:00" -> "22:30", "22:45:00" -> "22:45",
        "23:00:00" -> "23:00", "23:15:00" -> "23:15", "23:30:00" -> "23:30", "23:45:00" -> "23:45"
    )
  val hour_options = Seq(
        "0.00"  -> "0.00",  "0.25"  -> "0.25",  "0.50"  -> "0.50",  "0.75"  -> "0.75",
        "1.00"  -> "1.00",  "1.25"  -> "1.25",  "1.50"  -> "1.50",  "1.75"  -> "1.75",
        "2.00"  -> "2.00",  "2.25"  -> "2.25",  "2.50"  -> "2.50",  "2.75"  -> "2.75",
        "3.00"  -> "3.00",  "3.25"  -> "3.25",  "3.50"  -> "3.50",  "3.75"  -> "3.75",
        "4.00"  -> "4.00",  "4.25"  -> "4.25",  "4.50"  -> "4.50",  "4.75"  -> "4.75",
        "5.00"  -> "5.00",  "5.25"  -> "5.25",  "5.50"  -> "5.50",  "5.75"  -> "5.75",
        "6.00"  -> "6.00",  "6.25"  -> "6.25",  "6.50"  -> "6.50",  "6.75"  -> "6.75",
        "7.00"  -> "7.00",  "7.25"  -> "7.25",  "7.50"  -> "7.50",  "7.75"  -> "7.75",
        "8.00"  -> "8.00",  "8.25"  -> "8.25",  "8.50"  -> "8.50",  "8.75"  -> "8.75",
        "9.00"  -> "9.00",  "9.25"  -> "9.25",  "9.50"  -> "9.50",  "9.75"  -> "9.75",
        "10.00" -> "10.00", "10.25" -> "10.25", "10.50" -> "10.50", "10.75" -> "10.75",
        "11.00" -> "11.00", "11.25" -> "11.25", "11.50" -> "11.50", "11.75" -> "11.75",
        "12.00" -> "12.00", "12.25" -> "12.25", "12.50" -> "12.50", "12.75" -> "12.75",
        "13.00" -> "13.00", "13.25" -> "13.25", "13.50" -> "13.50", "13.75" -> "13.75",
        "14.00" -> "14.00", "14.25" -> "14.25", "14.50" -> "14.50", "14.75" -> "14.75",
        "15.00" -> "15.00", "15.25" -> "15.25", "15.50" -> "15.50", "15.75" -> "15.75",
        "16.00" -> "16.00", "16.25" -> "16.25", "16.50" -> "16.50", "16.75" -> "16.75",
        "17.00" -> "17.00", "17.25" -> "17.25", "17.50" -> "17.50", "17.75" -> "17.75",
        "18.00" -> "18.00", "18.25" -> "18.25", "18.50" -> "18.50", "18.75" -> "18.75",
        "19.00" -> "19.00", "19.25" -> "19.25", "19.50" -> "19.50", "19.75" -> "19.75",
        "20.00" -> "20.00", "20.25" -> "20.25", "20.50" -> "20.50", "20.75" -> "20.75",
        "21.00" -> "21.00", "21.25" -> "21.25", "21.50" -> "21.50", "21.75" -> "21.75",
        "22.00" -> "22.00", "22.25" -> "22.25", "22.50" -> "22.50", "22.75" -> "22.75",
        "23.00" -> "23.00", "23.25" -> "23.25", "23.50" -> "23.50", "23.75" -> "23.75"
  )
  val test_options = Seq(
    "test1" -> "test1","test2" -> "test2","test3" -> "test3"
  )

    var attendance_data = {
      get[Int]("id") ~
      get[String]("attendance_date")  ~
      get[Int]("date_type_id") ~
      get[String]("arrived_at")  ~
      get[String]("left_at") map {
        case id ~
             attendance_date ~
             date_type_id ~
             arrived_at ~
             left_at => New_Attendance_Data(id,attendance_date,date_type_id,arrived_at,left_at)
      }
    }
    var all_attendance_data = {
      get[Int]("id") ~
      get[String]("attendance_date")  ~
      get[Int]("date_type_id") ~
      get[String]("arrived_at")  ~
      get[String]("left_at") ~
      get[Option[Int]]("detail_id") ~
      get[Option[Int]]("attendance_id") ~
      get[Option[Int]]("order_id") ~
      get[Option[Int]]("work_type_id") ~
      get[Option[String]]("hours") ~
      get[Option[String]]("note") ~ 
      get[Int]("row_number") ~
      get[Int]("holiday_type_id") map {
        case id ~
             attendance_date ~
             date_type_id ~
             arrived_at ~
             left_at ~
             detail_id ~
             attendance_id ~
             order_id ~
             work_type_id ~
             hours ~
             note ~
             row_number ~
             holiday_type_id => AllAttendanceData(
               id,
               attendance_date,
               date_type_id,
               arrived_at,
               left_at,
               detail_id,
               attendance_id,
               order_id,
               work_type_id,
               hours,
               note,
               row_number,
               holiday_type_id
            )
      }
    }
    //基本勤怠データの存在チェック
    def CheckAttendanceData(EmployeeId:Int,StartDate: String,EndDate: String): Boolean = {
        var Attendances = DB.withConnection { implicit c =>
            SQL(
              """
              SELECT
                id
              FROM 
                attendances
              WHERE
                employee_id = {EmployeeId} AND attendance_date BETWEEN {StartDate} and {EndDate}
              ORDER BY
                attendance_date asc
              """
              ).on(
              'EmployeeId -> EmployeeId,
              'StartDate  -> StartDate,
              'EndDate  -> EndDate
            ).as(scalar[Int].*)
        }
        //print(CalendarDates.GetDatePortion(EndDate) + ":" +Attendances.length.toString+"\r\n")
        if(Attendances.length >= CalendarDates.GetDatePortion(EndDate).toInt)
        {
          return true
        }
        else
        {
          return false
        }
    }
    //基本勤怠データ取得
    def GetAttendanceData(EmployeeId:Int,start_date:String,end_date:String): List[New_Attendance_Data] = {
        DB.withConnection { implicit c =>
            SQL(
              """
              SELECT
                id,
                CONVERT(attendance_date,CHAR) AS 'attendance_date',
                date_type_id,
                CONVERT(arrived_at,CHAR) AS 'arrived_at',
                CONVERT(left_at,CHAR) AS 'left_at'
              from 
                attendances
              where
               employee_id = {EmployeeId} and attendance_date between {start_date} and {end_date}
              order by
                attendance_date asc
              """
              ).on(
              'EmployeeId -> EmployeeId,
              'start_date  -> start_date,
              'end_date    -> end_date
            ).as(NewAttendance.attendance_data*)
        }
    }
    //ユニークキーからIDを取得
    def GetAttendanceId(EmployeeId:Int,attendance_date:String): Option[Int] = {
        DB.withConnection { implicit c =>
            SQL(
              """
              SELECT
                id
              FROM
                attendances
              where
                employee_id = {EmployeeId} and attendance_date = {attendance_date}
              """
              ).on(
              'EmployeeId   -> EmployeeId,
              'attendance_date -> attendance_date
            ).as(scalar[Int].singleOpt)
        }
    }
    //勤怠データの初期登録
    def initialRegister(EmployeeId: Int,StartDate :String,EndDate :String,DefinedStartTime :String,DefinedEndTime :String,StandardWorkHour: String): Int = {
      //明細行を全削除
      WorkDetails.deleteAllWorkDetails(EmployeeId,StartDate,EndDate)
      //基本行を全削除
      deleteAllAttendances(EmployeeId,StartDate,EndDate)
      //カレンダーマスタから日付情報を取得
      var CalendarInfo = CalendarDates.getCalendarInfo(StartDate,EndDate)
      CalendarInfo.map { info =>
        val DefineWorkHour = if(CalendarDates.getIsHoliday(info.date_type_id) == 1) "0.00" else StandardWorkHour
        val DefineRestHour = if(CalendarDates.getIsHoliday(info.date_type_id) == 1) "0.00" else "1.00"
        val ArrivedAt      = if(CalendarDates.getIsHoliday(info.date_type_id) == 1) "00:00:59" else DefinedStartTime
        val LeftAt         = if(CalendarDates.getIsHoliday(info.date_type_id) == 1) "00:00:59" else DefinedEndTime
        //基本行初期登録
        SaveDailyAttendanceData(
          EmployeeId,
          info.date,
          info.date_type_id.toString,
          info.date+" "+ArrivedAt,
          info.date+" "+LeftAt,
          DefineWorkHour,
          DefineRestHour,
          "0.00",
          "0.00",
          "0.00",
          0
        )
        val attendance_id = NewAttendance.GetAttendanceId(EmployeeId,info.date).get
        //明細行初期登録
        WorkDetails.UpdateWorkDetailsData(
            0,
            attendance_id,
            1,
            1,
            "0.00",
            "",
            0
        )
      }
      return 0
    }
    //勤怠データ保存
    def SaveDailyAttendanceData(
      EmployeeId:Int,
      attendance_date:String,
      date_type_id:String,
      arrived_at:String,
      left_at:String,
      nomal_hours:String,
      rest_hours:String,
      overtime_hours:String,
      late_hours:String,
      fast_hours:String,
      is_locked:Int): Int = {
      DB.withConnection { implicit c =>
        SQL(
            """
            INSERT INTO attendances (
              employee_id,
              attendance_date,
              date_type_id,
              arrived_at,
              left_at,
              nomal_hours,
              rest_hours,
              overtime_hours,
              late_hours,
              fast_hours,
              is_locked,
              created_at,
              updated_at
            )
            VALUES (
              {EmployeeId},
              {attendance_date},
              {date_type_id},
              {arrived_at},
              {left_at},
              {nomal_hours},
              {rest_hours},
              {overtime_hours},
              {late_hours},
              {fast_hours},
              {is_locked},
              CURRENT_TIMESTAMP(),
              CURRENT_TIMESTAMP()
            )
            """
            ).on(
                  'EmployeeId      -> EmployeeId,
                  'attendance_date -> attendance_date,
                  'date_type_id    -> date_type_id,
                  'arrived_at      -> arrived_at,
                  'left_at         -> left_at,
                  'nomal_hours     -> nomal_hours,
                  'rest_hours      -> rest_hours,
                  'overtime_hours  -> overtime_hours,
                  'late_hours      -> late_hours,
                  'fast_hours      -> fast_hours,
                  'is_locked       -> is_locked
            ).executeUpdate()
        }
    }

    //基本勤怠データ更新
    def UpdateDailyAttendanceData(
      EmployeeId:Int,
      attendance_date:String,
      date_type_id:String,
      arrived_at:String,
      left_at:String,
      nomal_hours:String,
      rest_hours:String,
      overtime_hours:String,
      late_hours:String,
      fast_hours:String,
      is_locked:Int): Int = {
      DB.withConnection { implicit c =>
        SQL(
            """
            INSERT INTO attendances (
              employee_id,
              attendance_date,
              date_type_id,
              arrived_at,
              left_at,
              nomal_hours,
              rest_hours,
              overtime_hours,
              late_hours,
              fast_hours,
              is_locked,
              created_at,
              updated_at
            )
            VALUES (
              {EmployeeId},
              {attendance_date},
              {date_type_id},
              {arrived_at},
              {left_at},
              {nomal_hours},
              {rest_hours},
              {overtime_hours},
              {late_hours},
              {fast_hours},
              {is_locked},
              CURRENT_TIMESTAMP(),
              CURRENT_TIMESTAMP()
            )
            ON DUPLICATE KEY UPDATE
              arrived_at     = {arrived_at},
              left_at        = {left_at},
              nomal_hours    = {nomal_hours},
              rest_hours     = {rest_hours},
              overtime_hours = {overtime_hours},
              late_hours     = {late_hours},
              fast_hours     = {fast_hours},
              is_locked      = {is_locked},
              updated_at     = CURRENT_TIMESTAMP()
            """
        ).on(
              'EmployeeId      -> EmployeeId,
              'attendance_date -> attendance_date,
              'date_type_id    -> date_type_id,
              'arrived_at      -> arrived_at,
              'left_at         -> left_at,
              'nomal_hours     -> nomal_hours,
              'rest_hours      -> rest_hours,
              'late_hours      -> late_hours,
              'fast_hours      -> fast_hours,
              'overtime_hours  -> overtime_hours,
              'is_locked       -> is_locked
        ).executeUpdate()
      }
    }
    //基本勤怠データ取得
    def getAllAttendanceData(EmployeeId:Int,start_date:String,end_date:String): List[AllAttendanceData] = {
        var att_data = DB.withConnection { implicit c =>
            SQL(
              """
              SELECT
	              att.id AS id,
                CONVERT(att.attendance_date,CHAR) AS attendance_date,
                att.date_type_id AS date_type_id,
                CONVERT(att.arrived_at,CHAR) AS arrived_at,
                CONVERT(att.left_at,CHAR) AS left_at,
                det.id AS detail_id,
                det.attendance_id AS attendance_id,
                det.order_id AS order_id,
                det.work_type_id AS work_type_id,
                CONVERT(det.hours,CHAR) AS hours,
                det.note AS note,
                1 AS row_number,
                0 AS holiday_type_id
              FROM
	              attendances as att LEFT OUTER JOIN work_details as det ON att.id = det.attendance_id
              WHERE
	              att.employee_id = {EmployeeId} and att.attendance_date between {start_date} and {end_date}
              ORDER BY
                att.attendance_date asc
              """
              ).on(
              'EmployeeId -> EmployeeId,
              'start_date  -> start_date,
              'end_date    -> end_date
            ).as(NewAttendance.all_attendance_data *)
        }
        var past_date = ""
        var row_number  = 1
        val info: List[AllAttendanceData] = att_data.map { data =>
          //行番号の付与
          if(past_date != "" && past_date == data.attendance_date){
            row_number = row_number + 1
          }
          else{
            row_number = 1
          }
          past_date = data.attendance_date
          //休暇が設定されているかチェックする
          val holiday_type_id = DetailHolidays.getHolidayType(EmployeeId,data.attendance_date)
          //print("日付-"+data.attendance_date+"休暇種別ID:"+holiday_type_id+"\r\n")
          var order_id = data.order_id
          var work_type_id = data.work_type_id
          if(holiday_type_id != 0){
            val detail_info = DetailHolidays.getDetailInfo(EmployeeId,holiday_type_id)
            order_id      = detail_info(0).order_id
            work_type_id = detail_info(0).work_type_id
          }
          AllAttendanceData(
            data.id,
            data.attendance_date,
            data.date_type_id,
            data.arrived_at,
            data.left_at,
            data.detail_id,
            data.attendance_id,
            order_id,
            work_type_id,
            data.hours,
            data.note,
            row_number,
            holiday_type_id
            )
        }
        return info
    }
    //指定した年月の勤怠データを削除
    def deleteAllAttendances(EmployeeId: Int,StartDate :String,EndDate: String): Int = {
        DB.withConnection { implicit c =>
            SQL(
              """
              DELETE
              FROM 
	              attendances
              WHERE
                employee_id = {EmployeeId} AND attendance_date BETWEEN {StartDate} AND {EndDate}
              """
              ).on(
              'EmployeeId -> EmployeeId,
              'StartDate -> StartDate,
              'EndDate -> EndDate
            ).executeUpdate()
        }
    }
    //指定した年月日の勤怠データを削除
    def deleteAttendanceData(empID: Int, date: String): Int = {
        DB.withConnection { implicit c =>
            SQL(
              """
              DELETE
              FROM 
	              attendances
              WHERE
                employee_id = {empID}
              AND
                attendance_date = {date}
              """
              ).on(
              'empID -> empID,
              'date -> date
            ).executeUpdate()
        }
    }

    //勤怠データ取得(初回表示時)
    def getDefaultAttendanceData(EmployeeId: Int,StartDate:String,EndDate:String,DefinedStartTime: String,DefinedEndTime: String): List[AllAttendanceData] = {
        val employeeType = Employees.getEmployeeTypeById(EmployeeId)
        //明細行を全削除
        WorkDetails.deleteAllWorkDetails(EmployeeId,StartDate,EndDate)
        //基本行を全削除
        deleteAllAttendances(EmployeeId,StartDate,EndDate)
        var att_data = DB.withConnection { implicit c =>
            SQL(
              """
              SELECT
	              0 AS id,
                CONVERT(date,CHAR) AS attendance_date,
                date_type_id,
                CONCAT(date,' ',{DefinedStartTime}) AS arrived_at,
                CONCAT(date,' ',{DefinedEndTime}) AS left_at,
	              null AS detail_id,
	              null AS attendance_id,
	              null AS order_id,
	              null AS work_type_id,
	              null AS hours,
	              null AS note,
	              1 AS row_number,
                0 AS holiday_type_id
              FROM
	              calendar_dates
              WHERE
	              date between {StartDate} and {EndDate}
              ORDER BY
	              date asc,date_type_id asc
              """
              ).on(
              'StartDate -> StartDate,
              'EndDate   -> EndDate,
              'DefinedStartTime -> DefinedStartTime,
              'DefinedEndTime  -> DefinedEndTime
            ).as(NewAttendance.all_attendance_data *)
        }
        var past_date = ""
        val info: List[AllAttendanceData] = att_data.map { data =>
          //休暇が設定されているかチェックする
          val holiday_type_id = DetailHolidays.getHolidayType(EmployeeId,data.attendance_date)
          var id = data.id
          var order_id = data.order_id
          var work_type_id = data.work_type_id
          var left_at = data.left_at
          var date_type = data.date_type_id
          if(data.date_type_id == CalendarDates.PremiumFriday && EmployeeTypes.checkTargetPremiumFriday(employeeType)){
            left_at = data.attendance_date + " 15:30:00"
          }
          if(holiday_type_id != 0){
            val detail_info = DetailHolidays.getDetailInfo(EmployeeId,holiday_type_id)
            order_id      = detail_info(0).order_id
            work_type_id  = detail_info(0).work_type_id
          }
          else{
            //カレンダーマスタを参照して日付種別が２つ以上見つかった場合は日付種別を変更
            if(CalendarDates.getDateTypeCount(data.attendance_date) >= 2 && EmployeeTypes.checkTargetPremiumFriday(employeeType)){
              date_type = CalendarDates.PremiumFriday
              left_at = data.attendance_date + " 15:30:00"
            }
          }
          if(past_date == data.attendance_date){
            id = -1
          }
          past_date = data.attendance_date
          AllAttendanceData(
            id,
            data.attendance_date,
            date_type,
            data.arrived_at,
            left_at,
            data.detail_id,
            data.attendance_id,
            order_id,
            work_type_id,
            data.hours,
            data.note,
            data.row_number,
            holiday_type_id
          )
        }
      return info
    }

  //キンカン基本勤怠データ更新
    def UpdateDailyAttendanceDataNav(
                                      EmployeeId:Int,
                                      attendance_date:String,
                                      date_type_id:String,
                                      arrived_at:String,
                                      left_at:String,
                                      nomal_hours:String,
                                      rest_hours:String,
                                      overtime_hours:String,
                                      late_hours:String,
                                      fast_hours:String,
                                      is_locked:Int): Int = {
      DB.withConnection { implicit c =>
        SQL(
          """
           UPDATE temporary_employees
           SET arrived_at     = {arrived_at},
               left_at        = {left_at},
               nomal_hours    = {nomal_hours},
               rest_hours     = {rest_hours},
               overtime_hours = {overtime_hours},
               late_hours     = {late_hours},
               fast_hours     = {fast_hours},
               is_locked      = {is_locked}
            WHERE
              employee_id     = {EmployeeId},
              attendance_date = {attendance_date}
          """
        ).on(
          'EmployeeId      -> EmployeeId,
          'attendance_date -> attendance_date,
          'date_type_id    -> date_type_id,
          'arrived_at      -> arrived_at,
          'left_at         -> left_at,
          'nomal_hours     -> nomal_hours,
          'rest_hours      -> rest_hours,
          'late_hours      -> late_hours,
          'fast_hours      -> fast_hours,
          'overtime_hours  -> overtime_hours,
          'is_locked       -> is_locked
        ).executeUpdate()
      }
  }
  def checkCRUD(userID1: String, userID2: String): Boolean = {
    var check = false
    if(userID1 == userID2){ check = true } 
    return check
  }

  //勤怠データ存在チェック
  def isExist(empID: Int, date: String): Boolean = {
    val start = date + "-01"
    val end   = date + "-31"
    val cnt = DB.withConnection { implicit c =>
      SQL(
        """
          SELECT
            count(*)
          FROM 
	          attendances as att
		          LEFT JOIN
	          work_details as wd on att.id = wd.attendance_id
          WHERE
	          att.employee_id = {empID}
          AND
	          att.attendance_date between	{start} and {end}
          AND
	          wd.order_id not in (1,5)
          AND
	          wd.work_type_id != 1
        """
      ).on('empID -> empID,'start  -> start,'end  -> end).as(scalar[Int].single)
    }
    return if(cnt >= 1){ return true } else { false }
  }

  // 勤怠データロック
  def lockOneUserAttendance(empID: Int, from: String, to: String, status: Int): Int = {
    DB.withConnection { implicit c =>
      SQL(
        """
        UPDATE
        attendances
        SET
        is_locked = {status}
        WHERE
        attendance_date between {from} and {to}
        AND
        employee_id = {empID}
        """
        ).on('empID -> empID, 'from -> from, 'to -> to).executeUpdate()
    }
  }
}

using BookingWhizzAdmins.AngularModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace BookingWhizzAdmins.Controllers
{
    public class JsonAngController : Controller
    {
        // GET: JsonAng
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);//sqlServer Connection
        public ActionResult AllUsers()
        {
            if (Session["LoginType"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        public string GetAllUsers()
        {
            if (Session["LoginType"] == null)
            {
                return "Login Session Expair.!";
            }
            //return Json("Error", JsonRequestBehavior.AllowGet);
            try
            {
                string Status = "";
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("SP_Admin", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProcedureType", "G");
                cmd.Parameters.AddWithValue("@SessionType", "3");
                cmd.Parameters.AddWithValue("@p_UserTypeId", Session["UserTypeId"]);
                SqlParameter _results = new SqlParameter("@MessageOut", SqlDbType.VarChar, 500);
                _results.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(_results);

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                var set = adp.Fill(dt);
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }
                return serializer.Serialize(rows);
            }
            catch(Exception ex)
            {

                return "System Error : Contact Technical Support";
            } 
        }
        public ActionResult CreateUser()
        {
            if (Session["LoginType"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        public string GetUserType()
        {
            if (Session["LoginType"] == null)
            {
                return "Login Session Expair.!";
            }
            try
            {
                string Status = "";
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_Admin", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProcedureType", "G");
            cmd.Parameters.AddWithValue("@SessionType", "2");
            cmd.Parameters.AddWithValue("@p_UserTypeId", Session["UserTypeId"]);
            SqlParameter _results = new SqlParameter("@MessageOut", SqlDbType.VarChar, 500);
            _results.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(_results);

            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            con.Open();
            cmd.ExecuteNonQuery();
            Status = _results.Value.ToString();
            con.Close();
            var set = adp.Fill(dt);
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
            }
            catch (Exception ex)
            {

                return "System Error : Contact Technical Support";
            }
        }
        public JsonResult CreateUsers(Users us)
        {
            try
            {
                Random rnd = new Random();
                int randomNumber = rnd.Next(102, 13000);
                string Status = "";
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("SP_Admin", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProcedureType", "I");
                cmd.Parameters.AddWithValue("@SessionType", "1");
                cmd.Parameters.AddWithValue("@p_OwnerId", Session["OwnerId"]);
                cmd.Parameters.AddWithValue("@p_UserTypeId", us.UserTypeId);
                cmd.Parameters.AddWithValue("@p_UserName", us.UserNameAdd);
                cmd.Parameters.AddWithValue("@p_LoginId", us.LoginId);
                cmd.Parameters.AddWithValue("@p_Password", us.PasswordAdd);
                cmd.Parameters.AddWithValue("@p_ActivationCode", randomNumber);
                SqlParameter _result = new SqlParameter("@MessageOut", SqlDbType.VarChar, 500);
                _result.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(_result);

                con.Open();
                cmd.ExecuteNonQuery();
                Status = _result.Value.ToString();
                con.Close();
                if (Status == "Success")
                {
                    TempData["AddUsers"] = "User Created !";
                }
                else
                {
                    TempData["Error"] = "Data Is Invalid";
                }
                // 
                return Json(Status, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "System Error : Contact Technical Support";

                return Json("Error", JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult Accommodation()
        {
            if (Session["LoginType"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        public string AccommodationTypes()
        {
            if (Session["LoginType"] == null)
            {
                return "Login Session Expair.!";
            }
            try
            { 
            string Status = "";
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_Accommodations", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProcedureType", "G");
            cmd.Parameters.AddWithValue("@SessionType", "112");
            cmd.Parameters.AddWithValue("@p_MultiLanguageId", "1");
            SqlParameter _results = new SqlParameter("@MessageOut", SqlDbType.VarChar, 500);
            _results.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(_results);

            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            con.Open();
            cmd.ExecuteNonQuery();
            Status = _results.Value.ToString();
            con.Close();
            var set = adp.Fill(dt);
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
            }
            catch (Exception ex)
            {

                return "System Error : Contact Technical Support";
            }
        }
        public string TimeZone()
        {
            if (Session["LoginType"] == null)
            {
                return "Login Session Expair.!";
            }
            try
            {
            string Status = "";
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_Accommodations", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProcedureType", "G");
            cmd.Parameters.AddWithValue("@SessionType", "113");
            SqlParameter _results = new SqlParameter("@MessageOut", SqlDbType.VarChar, 500);
            _results.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(_results);

            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            con.Open();
            cmd.ExecuteNonQuery();
            Status = _results.Value.ToString();
            con.Close();
            var set = adp.Fill(dt);
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
            }
            catch (Exception ex)
            {

                return "System Error : Contact Technical Support";
            }
        }
        public string Country()
        {
            if (Session["LoginType"] == null)
            {
                return "Login Session Expair.!";
            }
            try
            { 
            string Status = "";
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_Accommodations", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProcedureType", "G");
            cmd.Parameters.AddWithValue("@SessionType", "103");
            cmd.Parameters.AddWithValue("@p_MultiLanguageId", "1");
            SqlParameter _results = new SqlParameter("@MessageOut", SqlDbType.VarChar, 500);
            _results.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(_results);

            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            con.Open();
            cmd.ExecuteNonQuery();
            Status = _results.Value.ToString();
            con.Close();
            var set = adp.Fill(dt);
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
            }
            catch (Exception ex)
            {

                return "System Error : Contact Technical Support";
            }
        }
        public string Cities(string CountryId)
        {
            if (Session["LoginType"] == null)
            {
                return "Login Session Expair.!";
            }
            try
            { 
            string Status = "";
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_Accommodations", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProcedureType", "G");
            cmd.Parameters.AddWithValue("@SessionType", "105");
            cmd.Parameters.AddWithValue("@p_MultiLanguageId", 1);
            cmd.Parameters.AddWithValue("@p_CountryId", CountryId);
            SqlParameter _results = new SqlParameter("@MessageOut", SqlDbType.VarChar, 500);
            _results.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(_results);

            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            con.Open();
            cmd.ExecuteNonQuery();
            Status = _results.Value.ToString();
            con.Close();
            var set = adp.Fill(dt);
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
            }
            catch (Exception ex)
            {

                return "System Error : Contact Technical Support";
            }
        }
        public string Currency()
        {
            if (Session["LoginType"] == null)
            {
                return "Login Session Expair.!";
            }
            try
            { 
            string Status = "";
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_Accommodations", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProcedureType", "G");
            cmd.Parameters.AddWithValue("@SessionType", "107");
            SqlParameter _results = new SqlParameter("@MessageOut", SqlDbType.VarChar, 500);
            _results.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(_results);

            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            con.Open();
            cmd.ExecuteNonQuery();
            Status = _results.Value.ToString();
            con.Close();
            var set = adp.Fill(dt);
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
            }
            catch (Exception ex)
            {

                return "System Error : Contact Technical Support";
            }
        }
        public JsonResult CreateAccommodations(SaveAccommodations Save_AC)
        {
            if (Session["LoginType"] == null)
            {
                return Json("Login Session TimeOut", JsonRequestBehavior.AllowGet);
            }
            try
            {
                string Status = "";
                SqlCommand cmd = new SqlCommand("SP_Accommodations", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProcedureType", "I");
                cmd.Parameters.AddWithValue("@SessionType", "1");
                cmd.Parameters.AddWithValue("@p_MultiLanguageId", "1");
                cmd.Parameters.AddWithValue("@p_OwnerId", Session["OwnerId"]);
                cmd.Parameters.AddWithValue("@p_AccommodationTypeId", Save_AC.AccommodationTypeId);
                cmd.Parameters.AddWithValue("@p_PropertyCategory", Save_AC.PropertyCategory);
                cmd.Parameters.AddWithValue("@p_AddressId", Save_AC.AddressId);
                cmd.Parameters.AddWithValue("@p_CurrencyId", Save_AC.CurrencyId);
                cmd.Parameters.AddWithValue("@p_ExtraFeeTypeId", Save_AC.ExtraFeeTypeId);
                cmd.Parameters.AddWithValue("@p_AccommodationName", Save_AC.AccommodationName);
                cmd.Parameters.AddWithValue("@p_TimeZoneId", Save_AC.TimeZoneId);
                cmd.Parameters.AddWithValue("@p_ContactNo", Save_AC.ContactNo);
                cmd.Parameters.AddWithValue("@p_EmailId", Save_AC.EmailId);
                cmd.Parameters.AddWithValue("@p_PostCode", Save_AC.PostCode);
                cmd.Parameters.AddWithValue("@p_Latitude", Save_AC.Latitude);
                cmd.Parameters.AddWithValue("@p_Longitude", Save_AC.Longitude);
                cmd.Parameters.AddWithValue("@p_WebSearch", Save_AC.SearchEngine);
                SqlParameter _results = new SqlParameter("@MessageOut", SqlDbType.VarChar, 500);
                _results.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(_results);

                con.Open();
                cmd.ExecuteNonQuery();
                Status = _results.Value.ToString();
                if (Status == "")
                {
                    TempData["Error"] = "Server Error : Contact Technical Support";
                }
                else
                {

                    if (int.Parse(Session["WebSearch"].ToString()) == 1)
                    {
                        HttpWebRequest request = WebRequest.Create("http://roomph.pk/admin/home/fetch-accommodations/" + Session["AccommodationId"]) as HttpWebRequest;
                        HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                        Stream stream = response.GetResponseStream();
                    }
                    TempData["okey"] = "Accommodation Created!";

                    DataTable dt = new DataTable();
                    SqlCommand cmd6 = new SqlCommand("SP_Accommodations", con);
                    cmd6.CommandType = CommandType.StoredProcedure;
                    cmd6.Parameters.AddWithValue("@ProcedureType", "G");
                    cmd6.Parameters.AddWithValue("@SessionType", "202");
                    cmd6.Parameters.AddWithValue("@p_MultiLanguageId", "1");
                    cmd6.Parameters.AddWithValue("@p_UserId", Session["UserId"]);
                    SqlParameter _results6 = new SqlParameter("@MessageOut", SqlDbType.VarChar, 500);
                    _results6.Direction = ParameterDirection.Output;
                    cmd6.Parameters.Add(_results6);
                    SqlDataAdapter adp6 = new SqlDataAdapter(cmd6);
                    adp6.Fill(dt);
                    Session["SaveDt"] = dt;
                    //
                    int UserTypeId = int.Parse(Session["UserTypeId"].ToString());
                    if (UserTypeId == 5)
                    {
                        con.Open();
                        cmd6.ExecuteNonQuery();
                        SqlDataReader dr12 = cmd6.ExecuteReader();
                        if (dr12.HasRows)
                        {
                            while (dr12.Read())
                            {
                                Session["WebSearch"] = Save_AC.SearchEngine;
                                Session["AccommodationId"] = dr12["AccommodationId"];
                                Session["AccommodationName"] = dr12["AccommodationName"];
                            }
                        }
                        con.Close();
                    }

                }
                con.Close();
                return Json(Status, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "System Error : Contact Technical Support";
                return Json("System Error : Contact Technical Support", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UpdateAccommodation()
        {
            if (Session["LoginType"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        public string ExtraFee()
        {
            if (Session["LoginType"] == null)
            {
                return "Login Session Expair.!";
            }
            try { 
            string Status = "";
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_Accommodations", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProcedureType", "G");
            cmd.Parameters.AddWithValue("@SessionType", "110");
            cmd.Parameters.AddWithValue("@p_UserTypeId", Session["UserTypeId"]);
            SqlParameter _results = new SqlParameter("@MessageOut", SqlDbType.VarChar, 500);
            _results.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(_results);

            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            con.Open();
            cmd.ExecuteNonQuery();
            Status = _results.Value.ToString();
            con.Close();
            var set = adp.Fill(dt);
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
            }
            catch (Exception ex)
            {

                return "System Error : Contact Technical Support";
            }
        }
        public string GetAccommodationDetails()
        {
            if (Session["LoginType"] == null)
            {
                return "Login Session Expair.!";
            }
            try
            { 
            var LanguageId = 1;// Session["LanguageId"];
            string Status = "";
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_Accommodations", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProcedureType", "G");
            cmd.Parameters.AddWithValue("@SessionType", "1");
            cmd.Parameters.AddWithValue("@p_MultiLanguageId", LanguageId);
            cmd.Parameters.AddWithValue("@p_AccommodationId", Session["AccommodationId"]);
            cmd.Parameters.AddWithValue("@p_UserTypeId", Session["UserTypeId"]);
            cmd.Parameters.AddWithValue("@p_UserId", Session["UserId"]);
            SqlParameter _results = new SqlParameter("@MessageOut", SqlDbType.VarChar, 500);
            _results.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(_results);

            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            con.Open();
            cmd.ExecuteNonQuery();
            Status = _results.Value.ToString();
            con.Close();
            var set = adp.Fill(dt);
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
            }
            catch (Exception ex)
            {

                return "System Error : Contact Technical Support";
            }
        }
        public JsonResult UpdateAccommodations(UpdateAccommodation UD_AC)
        {
            var LanguageId = 1;// Session["LanguageId"];
            if (Session["LoginType"] == null)
            {
                return Json("Login Session TimeOut", JsonRequestBehavior.AllowGet);
            }
            if (Session["AccommodationId"] == null || Session["AccommodationId"].ToString() == "")
            {
                return Json("Accommodation Not Found !", JsonRequestBehavior.AllowGet);
            }
            try
            {
                string Status = "";
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("SP_Accommodations", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProcedureType", "M");
                cmd.Parameters.AddWithValue("@SessionType", "1");
                cmd.Parameters.AddWithValue("@p_MultiLanguageId", LanguageId);
                cmd.Parameters.AddWithValue("@p_AccommodationId", Session["AccommodationId"]);
                cmd.Parameters.AddWithValue("@p_CurrencyId", UD_AC.CurrencyId);
                cmd.Parameters.AddWithValue("@p_ExtraFeeTypeId", UD_AC.ExtraFeeTypeId);
                cmd.Parameters.AddWithValue("@p_PropertyCategory", UD_AC.PropertyCategory);
                cmd.Parameters.AddWithValue("@p_TimeZoneId", UD_AC.TimeZoneId);
                cmd.Parameters.AddWithValue("@p_ContactNo", UD_AC.ContactNo);
                cmd.Parameters.AddWithValue("@p_EmailId", UD_AC.EmailId);
                cmd.Parameters.AddWithValue("@p_PostCode", UD_AC.PostCode);
                cmd.Parameters.AddWithValue("@p_Latitude", UD_AC.Latitude);
                cmd.Parameters.AddWithValue("@p_Longitude", UD_AC.Longitude);
                cmd.Parameters.AddWithValue("@p_WebSearch", UD_AC.SearchEngine);
                SqlParameter _results = new SqlParameter("@MessageOut", SqlDbType.VarChar, 500);
                _results.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(_results);

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                con.Open();
                cmd.ExecuteNonQuery();
                Status = _results.Value.ToString();
                adp.Fill(ds, cmd.CommandText);
                if (Status == "")
                {
                    TempData["Error"] = "Server Error : Contact Technical Support";
                }
                else
                {
                    Session["WebSearch"] = UD_AC.SearchEngine;
                    if (int.Parse(Session["WebSearch"].ToString()) == 1)
                    {
                        HttpWebRequest request = WebRequest.Create("http://roomph.pk/admin/home/fetch-accommodations/" + Session["AccommodationId"]) as HttpWebRequest;
                        HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                        Stream stream = response.GetResponseStream();
                    }
                    //TempData["okey"] = "Accommodation Update Successfully!";
                }
                con.Close();
                return Json("Accommodation Update Successfully", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "System Error : Contact Technical Support";
                return Json("System Error : Contact Technical Support", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult UserMapping()
        {
            if (Session["LoginType"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        public string MappedAccommodationUser(int UserId)
        {
            if (Session["LoginType"] == null)
            {
                return "Login Session Expair.!";
            }
            try
            { 
            var LanguageId = 1;//Session["LanguageId"];  

                var asdas = TempData["new"];
                string Status = "";
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("SP_Accommodations", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProcedureType", "G");
                cmd.Parameters.AddWithValue("@SessionType", "202");
                cmd.Parameters.AddWithValue("@p_MultiLanguageId", LanguageId);
                cmd.Parameters.AddWithValue("@p_UserId", UserId);
                SqlParameter _results = new SqlParameter("@MessageOut", SqlDbType.VarChar, 500);
                _results.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(_results);

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                con.Open();
                cmd.ExecuteNonQuery();
                Status = _results.Value.ToString();
                con.Close();
                var set = adp.Fill(dt);
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }
          
                return serializer.Serialize(rows);
            }
            catch (Exception ex)
            {

                return "System Error : Contact Technical Support";
            }
        } 
        public JsonResult RemoveMappingAccommodations(int UserId, int AccommodationId)
        { 
            try
            {
                string Status6 = "";
                DataSet ds6 = new DataSet();
                SqlCommand cmd6 = new SqlCommand("SP_Accommodations", con);
                cmd6.CommandType = CommandType.StoredProcedure;
                cmd6.Parameters.AddWithValue("@ProcedureType", "D");
                cmd6.Parameters.AddWithValue("@SessionType", "201");
                cmd6.Parameters.AddWithValue("@p_UserId", UserId);
                cmd6.Parameters.AddWithValue("@p_AccommodationId", AccommodationId);
                SqlParameter _results6 = new SqlParameter("@MessageOut", SqlDbType.VarChar, 500);
                _results6.Direction = ParameterDirection.Output;
                cmd6.Parameters.Add(_results6);

                SqlDataAdapter adp6 = new SqlDataAdapter(cmd6);
                con.Open();
                cmd6.ExecuteNonQuery();
                Status6 = _results6.Value.ToString();
                adp6.Fill(ds6);
                con.Close();

                TempData["okey"] = "Accommodation Assign Removed Successfully!";

                return Json("Accommodation Assign Removed Successfully!",JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            { 
                return Json("System Error : Contact Technical Support", JsonRequestBehavior.AllowGet);
            }

        }
        DataTable LoadAccommodations;
        public string LoadAccommodation(string UserId)
        {
            if (Session["LoginType"] == null)
            {
                return "Login Session Expair.!";
            }
            try
            { 
            LoadAccommodations = (DataTable)Session["LoadAccommodations"];
            if (LoadAccommodations != null && LoadAccommodations.Rows.Count > 0)
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                foreach (DataRow dr in LoadAccommodations.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in LoadAccommodations.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }
                return serializer.Serialize(rows);
            }
            else
            {
                var LanguageId = 1;// Session["LanguageId"];
                string Status6 = "";
                DataTable dt = new DataTable();
                SqlCommand cmd6 = new SqlCommand("SP_Accommodations_Test", con);
                cmd6.CommandType = CommandType.StoredProcedure;
                cmd6.Parameters.AddWithValue("@ProcedureType", "G");
                cmd6.Parameters.AddWithValue("@SessionType", "201");
                cmd6.Parameters.AddWithValue("@p_MultiLanguageId", LanguageId);
                cmd6.Parameters.AddWithValue("@p_UserId", Session["UserId"]);
                cmd6.Parameters.AddWithValue("@p_Assign_UserId", UserId);
                SqlDataAdapter da3 = new SqlDataAdapter(cmd6);
                var set = da3.Fill(dt); Session["LoadAccommodations"] = dt;
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }
                return serializer.Serialize(rows);
            }
            }
            catch (Exception ex)
            { 
                return "System Error : Contact Technical Support";
            }
        }
        public JsonResult MappingAccommodation(MapAccommodation AM)
        {
            var LanguageId = 1;// Session["LanguageId"];
            try
            { 
                int x = AM.MappingAccommodationId.Count() - 1;
                for (int i = 0; i <= x; i++)
                {

                    string Status6 = "";

                    DataSet ds6 = new DataSet();
                    SqlCommand cmd6 = new SqlCommand("SP_Accommodations_Test", con);
                    cmd6.CommandType = CommandType.StoredProcedure;
                    cmd6.Parameters.AddWithValue("@ProcedureType", "I");
                    cmd6.Parameters.AddWithValue("@SessionType", "201");
                    cmd6.Parameters.AddWithValue("@p_MultiLanguageId", LanguageId);
                    cmd6.Parameters.AddWithValue("@p_OwnerId", Session["OwnerId"]);
                    cmd6.Parameters.AddWithValue("@p_UserId", AM.UserId);
                    cmd6.Parameters.AddWithValue("@p_AccommodationId", AM.MappingAccommodationId[i]);
                    SqlParameter _results6 = new SqlParameter("@MessageOut", SqlDbType.VarChar, 500);
                    _results6.Direction = ParameterDirection.Output;
                    cmd6.Parameters.Add(_results6);

                    SqlDataAdapter adp6 = new SqlDataAdapter(cmd6);
                    con.Open();
                    cmd6.ExecuteNonQuery();
                    Status6 = _results6.Value.ToString();
                    adp6.Fill(ds6);
                    if (Status6 == "")
                    {
                        TempData["Error"] = "Server Error : Contact Technical Support";
                    }
                    else
                    {

                        TempData["okey"] = "Accommodation Mapped Successfully!";
                    }
                    con.Close(); 
                }

                return Json("Accommodation Mapped Successfully!", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "System Error : Contact Technical Support";

                return Json("System Error : Contact Technical Support", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult AccommodationDetails()
        {
            return View();
        }
        //public string GetAccommodationDetails()
        //{
        //    var LanguageId = 1;// Session["LanguageId"];
         
        //        try
        //        {
        //            string Status1 = "";
        //            DataTable dt = new DataTable();
        //            SqlCommand cmd1 = new SqlCommand("SP_Accommodations_Test", con);
        //            cmd1.CommandType = CommandType.StoredProcedure;
        //            cmd1.Parameters.AddWithValue("@ProcedureType", "G");
        //            cmd1.Parameters.AddWithValue("@SessionType", "1");
        //            cmd1.Parameters.AddWithValue("@p_MultiLanguageId", LanguageId);
        //            cmd1.Parameters.AddWithValue("@p_AccommodationId", Session["AccommodationId"]);
        //            cmd1.Parameters.AddWithValue("@p_UserTypeId", Session["UserTypeId"]);
        //            cmd1.Parameters.AddWithValue("@p_UserId", Session["UserId"]);
        //            SqlParameter _results1 = new SqlParameter("@MessageOut", SqlDbType.VarChar, 500);
        //            _results1.Direction = ParameterDirection.Output;
        //            cmd1.Parameters.Add(_results1);

        //            SqlDataAdapter adp = new SqlDataAdapter(cmd1);
        //            con.Open();
        //            cmd1.ExecuteNonQuery();
        //            Status1 = _results1.Value.ToString();
        //        con.Close();
        //        var set = adp.Fill(dt);
        //        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        //        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        //        Dictionary<string, object> row;
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            row = new Dictionary<string, object>();
        //            foreach (DataColumn col in dt.Columns)
        //            {
        //                row.Add(col.ColumnName, dr[col]);
        //            }
        //            rows.Add(row);
        //        }
        //        return serializer.Serialize(rows);
        //    }
        //        catch (Exception ex)
        //        {
        //          return  "System Error : Contact Technical Support"; 
        //        }
        //}
    }
}
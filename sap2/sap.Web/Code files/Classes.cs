using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Microsoft.Health;
using Microsoft.Health.Web;
using Microsoft.Health.ItemTypes;
using AKAV1;
namespace AKAV1
{
    class Operations
    {
        public static string connectionString = "Data Source=KSHITIJ-PC\\SQLEXPRESS;Initial Catalog=AKAV1;Integrated Security=True;MultipleActiveResultSets=True ";
        public enum CASE_STATUS { INSPECTION_PENDING = 0, PRESCRIPTION_GIVEN = 1, PARAMETER_REQUESTED = 2, PARAMETER_REQUESTED_PRESCRIPTION_GIVEN = 3, CLOSED = 4 };
        //        const int CASE_INSPECTION_PENDING=0;
        //        const int CASE_PRESCRIPTION_GIVEN=1;
        //       const int CASE_PARAMETER_REQUESTED=2;
        //      const int CASE_PARAMETER_REQUESTED_PRESCRIPTION_GIVEN = 3;

        public static void Main()
        {
            //allocateDoctorToCase(1);
            //Guid patientId = new Guid("a739c043-51b5-429f-897c-b6af59f0abb9");
            //DateTime d=new DateTime(2010,2,17,18,20,34);
            //    decimal caseId = createNewCase(patientId, "TC1");
            //Console.WriteLine(allocateDoctorToCase(4));
            //givePrescription(1,"med1",2,"mg","instr1","doc1","medc1",d);
           // Console.Write(diagonizeDisease(1, "doc1", "fever", new DateTime(2010, 2, 21, 12, 9, 10)));
            //  PendingCases[] cases = getPendingCases("doc2");
            //    Prescription p=new Prescription(cases[0].caseId,"Medicine1",10,"mg","instructions",cases[0].doctorId,DateTime.Now);
            //    Console.WriteLine(givePrescription(p,cases[0].dateTimeAllotted)); 
            //   for (int i = 0; i < d.Length; i++)
            //      Console.WriteLine(d[i]);
            // Console.WriteLine(diagonizeDisease(2,"doc2","fever"));

            //MedicalCase c = getCaseDetails(2);
            //Console.WriteLine(c);

            // Console.WriteLine(requestParameter(1,2));

            // Console.WriteLine(addParameterToCase(1,2));
            

        }
        public static bool login(string id, string password, int type)
        {
            bool boolStatus = true;
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader reader1 = null;
            string queryString = "";
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                switch (type)
                {
                    case 1:
                        queryString = "select * from doctor where doctorid='" + id + "' and password='" + password + "'";
                        break;
                    case 2:
                        queryString = "select * from patient where patientid='" + id + "' and password='" + password + "'";
                        break;
                    case 3:
                        queryString = "select * from testcentre where testcentreid='" + id + "' and password='" + password + "'";
                        break;
                    case 4:
                        queryString = "select * from healthofficial where healthofficialid='" + id + "' and password='" + password + "'";
                        break;
                }

                cmd = new SqlCommand(queryString, con);
                reader1 = cmd.ExecuteReader();
                if (!reader1.HasRows)
                    boolStatus = false;
            }
            catch (SqlException e)
            {
                System.Console.Write(e);

            }
            finally
            {
                if (con != null)
                    con.Close();
            }
            return (boolStatus);
        }
        public static QuestionAnswer[] getQuestionAnswer(decimal caseId)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader reader1 = null;
            QuestionAnswer[] qa = null;
            int i;
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                cmd = new SqlCommand("select qid,question,answer,status,date_time,(select count(*) from question_case where caseId="+caseId+") as count from  question_case where caseid=" + caseId,con);
                reader1 = cmd.ExecuteReader();
                if (reader1.HasRows)
                {
                    reader1.Read();
                    qa = new QuestionAnswer[(int)reader1["count"]];
                    i = 0;
                    do
                    {
                        qa[i] = new QuestionAnswer((string)reader1["question"], (string)reader1["answer"], caseId, (decimal)reader1["qid"], (int)reader1["status"], (DateTime)reader1["date_time"]);
                        i++;
                    }while(reader1.Read());
                    
                }
            }
            catch(SqlException e)
            {
                System.Console.WriteLine(e);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
            return(qa);
        }
        public static void askQuestion(string question, decimal caseId)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader reader1 = null;
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                cmd = new SqlCommand("select caseid from cases where caseid=" + caseId,con);
                reader1 = cmd.ExecuteReader();
                if(reader1.HasRows)
                {
                    reader1.Close();
                    decimal questionId = 1;
                    cmd.CommandText = "select qid from question_case where caseid="+caseId+" order by qid desc";
                    reader1 = cmd.ExecuteReader();
                    if (reader1.HasRows)
                    {
                        reader1.Read();
                        questionId=(decimal)reader1["qid"]+1;
                    }
                    reader1.Close();        
                    cmd.CommandText = "insert into question_case values("+questionId+","+caseId+",'"+DateTime.Now+"','"+question +"','',0)";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                System.Console.WriteLine("SqlException in askQuestion:"+e);
            }
            finally
            {
                if (con != null)
                    con.Close();
                
            }
        }
        public static void answerQuestion(decimal qid, decimal caseId, string answer)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                cmd = new SqlCommand("update question_case set answer='"+answer+"',status=1 where qid="+qid+" and caseId="+caseId, con);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                System.Console.WriteLine("sqlexception in answerQuestion :"+e);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
        public static Dictionary<decimal,string> getQuestions(decimal caseId)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader reader1 = null;
            Dictionary<decimal,string>  questions=new Dictionary<decimal,string>();
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                cmd = new SqlCommand("select * from question_case  where status=0 and caseid="+caseId, con);
                reader1 = cmd.ExecuteReader();
                while (reader1.Read())
                {
                     questions.Add((decimal)reader1["qid"],(string )reader1["question"]);
                }
            }
            catch (SqlException e)
            {
                System.Console.WriteLine("Sqlexception in getQuestions:"+e);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
            return(questions);
        }
        public static void closeCase(decimal caseId,string doctorId,DateTime dtAllotted)
        {
            SqlConnection con=null;
            SqlCommand cmd=null;
            SqlDataReader reader1=null;
            try
            {
                con=new SqlConnection(connectionString);
                con.Open();
                cmd=new SqlCommand("update cases set status="+(int)CASE_STATUS.CLOSED+" where caseid="+caseId,con);
                cmd.ExecuteNonQuery();
                cmd.CommandText="update doctor_case set attended_status=1,date_time_attended='"+DateTime.Now+"' where doctorid='"+doctorId+"' and caseid="+caseId+" and date_time_allotted='"+dtAllotted+"'";
                cmd.ExecuteNonQuery();
            }
            catch(SqlException e)
            {
                System.Console.WriteLine("SqlException in closeCase:"+e);
            }
            finally
            {
                 if(con!=null)
                  con.Close();

            }
        }
        public static bool allocateDoctorToCase(decimal caseId)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader reader1 = null;
            string querystring = null;
            string doctorId = null;
            bool boolstatus = true;
            try
            {

                con = new SqlConnection(connectionString);
                con.Open();
                querystring = "select caseid from cases where caseid=" + caseId;
                cmd = new SqlCommand(querystring, con);
                reader1 = cmd.ExecuteReader();
                if (reader1.HasRows)
                {
                    reader1.Close();
                    querystring = "select doctorid from doctor where doctorid not in(select doctorid from doctor_case where attended_status=0)";
                    cmd = new SqlCommand(querystring, con);
                    reader1 = cmd.ExecuteReader();
                    if (reader1.HasRows)
                    {
                        reader1.Read();
                        doctorId = (string)reader1["doctorid"];
                    }
                    else
                    {
                        reader1.Close();
                        querystring = "select doctorid from doctor_case where attended_status=0 group by doctorid having count(*)<=all" +
                    "(select count(*) from doctor_case where attended_status=0 group by doctorid)";
                        cmd.CommandText = querystring;
                        reader1 = cmd.ExecuteReader();
                        if (reader1.HasRows)
                        {
                            reader1.Read();
                            doctorId = (string)reader1["doctorid"];
                        }
                        else
                            boolstatus = false;
                        reader1.Close();
                    }
                    reader1.Close();
                    if (boolstatus)
                    {
                        querystring = "insert into doctor_case values(" + caseId + ",'" + doctorId + "','" + DateTime.Now + "','',0)";
                        cmd.CommandText = querystring;
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    reader1.Close();
                    boolstatus = false;
                }
            }
            catch (SqlException e)
            {
                System.Console.WriteLine("Sqlexception in allocatedoctortocase:" + e);
                boolstatus = false;
            }
            finally
            {
                con.Close();
            }
            return (boolstatus);
        }
        public static bool givePrescription(Prescription p, DateTime date_time_alloted)
        {
            bool boolStatus = true;
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader reader1 = null;
            string querystring = null;
            string medCentreId = null;
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                querystring = "select caseid from cases where caseid=" + p.caseId;
                cmd = new SqlCommand(querystring, con);
                reader1 = cmd.ExecuteReader();
                if (reader1.HasRows)
                {
                    reader1.Close();
                    querystring = "select doctorid from doctor where doctorid='" + p.doctorId + "'";
                    cmd = new SqlCommand(querystring, con);
                    reader1 = cmd.ExecuteReader();
                    if (reader1.HasRows)
                    {
                        reader1.Close();
                        medCentreId = findMedCentreId(p.caseId);
                        querystring = "select medcentreid from medcentre where medcentreid='" + medCentreId + "'";
                        cmd = new SqlCommand(querystring, con);
                        reader1 = cmd.ExecuteReader();
                        if (reader1.HasRows)
                        {
                            reader1.Close();
                            querystring = "insert into prescription values(" + p.caseId + ",'" + p.medicine + "','" + DateTime.Now + "'," +
                                p.quantity + ",'" + p.unit + "','" + p.instructions + "','" + p.doctorId + "','" + medCentreId + "')";
                            cmd = new SqlCommand(querystring, con);
                            cmd.ExecuteNonQuery();
                            querystring = "update doctor_case set attended_status=1,date_time_attended='" + DateTime.Now + "' where caseid=" + p.caseId +
                                " and doctorid='" + p.doctorId + "' and date_time_allotted='" + date_time_alloted + "'";
                            cmd = new SqlCommand(querystring, con);
                            cmd.ExecuteNonQuery();
                            querystring = "update cases set status=" + (int)CASE_STATUS.PRESCRIPTION_GIVEN + " where caseid=" + p.caseId;
                            cmd = new SqlCommand(querystring, con);
                            cmd.ExecuteNonQuery();
                        }
                        else
                            boolStatus = false;
                    }
                    else
                        boolStatus = false;
                }
                else
                    boolStatus = false;
                reader1.Close();
            }
            catch (SqlException e)
            {
                System.Console.WriteLine("SqlException in givePrescription:" + e);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
            return (boolStatus);
        }
        public static bool diagonizeDisease(decimal caseId, string doctorId, string disease, DateTime dateTimeAllotted,HealthRecordInfo hri)
        {
            bool boolStatus = true;
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader reader1 = null;
            string querystring = null;
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                querystring = "select caseid from cases where caseid=" + caseId + "";
                cmd = new SqlCommand(querystring, con);
                reader1 = cmd.ExecuteReader();
                if (reader1.HasRows)
                {
                    reader1.Close();
                    querystring = "select doctorid from doctor where doctorid='" + doctorId + "'";
                    cmd = new SqlCommand(querystring, con);
                    reader1 = cmd.ExecuteReader();
                    if (reader1.HasRows)
                    {
                        reader1.Close();
                        querystring = "select disease from disease where disease='" + disease + "'";
                        cmd = new SqlCommand(querystring, con);
                        reader1 = cmd.ExecuteReader();
                        if (reader1.HasRows)
                        {
                            reader1.Close();
                            querystring = "insert into disease_case values(" + caseId + ",'" + disease + "','" + DateTime.Now + "','" + doctorId + "')";
                            cmd = new SqlCommand(querystring, con);
                            cmd.ExecuteNonQuery();
                            updateDiseaseIncidence(caseId, disease,dateTimeAllotted,hri);
                        }
                        else
                            boolStatus = false;
                    }
                    else
                        boolStatus = false;
                }
                else
                    boolStatus = false;
            }
            catch (SqlException e)
            {
                System.Console.WriteLine("SqlException in diagonizedisease:" + e);
                boolStatus = false;
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
            return (boolStatus);
        }
        public static void updateDiseaseIncidence(decimal caseId, string disease, DateTime dateTimeAllotted,HealthRecordInfo hri)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader reader1 = null;
            SqlCommand cmd2 = null;
            SqlDataReader reader2 = null;
            string queryString = "";
            decimal locId = -1;
            decimal TimeId = -1;
            int parameterId;
            string parameterValue = "";
            int quarter;
            int i;
            decimal[] combinationIds = null;
            decimal combinationId;
            int[] criteriaId = null;
            string strcombinationIds = "";
            Guid patientId = Guid.Empty;
            HashSet<decimal> hs1 = new HashSet<decimal>();
            HashSet<decimal> hs2 = new HashSet<decimal>();
            string[] diseases = null;
            decimal[] occurrences = null;
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                queryString = "select patientId from Patient_case where caseId=" + caseId;
                cmd = new SqlCommand(queryString, con);
                reader1 = cmd.ExecuteReader();
                reader1.Read();
                patientId = (Guid)reader1["patientId"];
                reader1.Close();
                queryString = "select locationid from testcentre where testcentreid=(select testcentreid from cases where caseid=" + caseId + ")";
                cmd = new SqlCommand(queryString, con);
                reader1 = cmd.ExecuteReader();
                if (reader1.HasRows)
                {
                    reader1.Read();
                    locId = (decimal)reader1["locationid"];
                }
                reader1.Close();
                queryString = "select Timeid from time where month=" + dateTimeAllotted.Month + " and year=" + dateTimeAllotted.Year;
                cmd.CommandText = queryString;
                reader1 = cmd.ExecuteReader();
                if (!reader1.HasRows)
                {
                    TimeId = generateTimeId();

                    if (dateTimeAllotted.Month >= 1 && dateTimeAllotted.Month <= 3)
                        quarter = 1;
                    else if (dateTimeAllotted.Month >= 4 && dateTimeAllotted.Month <= 6)
                        quarter = 2;
                    else if (dateTimeAllotted.Month >= 7 && dateTimeAllotted.Month <= 9)
                        quarter = 3;
                    else
                        quarter = 4;
                    reader1.Close();
                    queryString = "insert into time values(" + TimeId + "," + dateTimeAllotted.Year + "," + quarter + "," + dateTimeAllotted.Month + ")";
                    cmd.CommandText = queryString;
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    reader1.Read();
                    TimeId=(decimal)reader1["timeid"];
                    reader1.Close();
                }
                
                queryString = "select distinct parameterId,(select count(distinct parameterid) from parameter_case where caseid=" + caseId + ") as count from Parameter_case where caseid=" + caseId;
                cmd = new SqlCommand(queryString, con);
                reader1 = cmd.ExecuteReader();
                if (reader1.HasRows)
                {
                    reader1.Read();
                    criteriaId = new int[(int)reader1["count"]];
                    i = 0;
                    do
                    {
                        parameterId = (int)reader1["parameterid"];
                        parameterValue =(new ParameterValues()).get_Parameter(parameterId, hri);
                         queryString = "select criteriaid from criteria where parameterid=" + parameterId + " and lowerbound<=" + parameterValue + " and upperbound>=" + parameterValue;
                        cmd2 = new SqlCommand(queryString, con);
                        reader2 = cmd2.ExecuteReader();
                        if(reader2.Read())
                         criteriaId[i] = (int)reader2["criteriaid"];
                        reader2.Close();
                        i++;
                    } while (reader1.Read());
                }
                reader1.Close();
                if (criteriaId == null)
                    return;
                queryString = "select combinationId from combination";
                cmd.CommandText = queryString;
                reader1 = cmd.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        hs1.Add((decimal)reader1["combinationid"]);
                    }
                    reader1.Close();
                    for (i = 0; i < criteriaId.Length; i++)
                    {
                        queryString = "select combinationid from combination_criteria where criteriaid=" + criteriaId[i];
                        cmd.CommandText = queryString;
                        reader1 = cmd.ExecuteReader();
                        while (reader1.Read())
                        {
                            hs2.Add((decimal)reader1["combinationid"]);
                        }
                        hs1.IntersectWith(hs2);
                        hs2.Clear();
                        reader1.Close();
                    }
                    if (hs1.Count == 0)
                    {
                        cmd.CommandText = "select combinationid from combination order by combinationid desc";
                        reader1=cmd.ExecuteReader();
                        reader1.Read();
                        decimal ncombinationId=1;
                        ncombinationId = (decimal)reader1["combinationid"] + 1;
                        reader1.Close();
                        cmd.CommandText = "insert into combination values("+ncombinationId+")";
                        cmd.ExecuteNonQuery();
                        for (i = 0; i < criteriaId.Length; i++)
                        {
                            cmd.CommandText = "insert into combination_criteria values("+ncombinationId+"," + criteriaId[i] + ")";
                            cmd.ExecuteNonQuery();
                        }
                        hs1.Add(ncombinationId);
                    }
                }
                else
                {
                    reader1.Close();
                    cmd.CommandText = "insert into combination values(1)";
                    cmd.ExecuteNonQuery();
                    for(i=0;i<criteriaId.Length;i++)
                    {
                        cmd.CommandText = "insert into combination_criteria values(1," + criteriaId[i] + ")";
                        cmd.ExecuteNonQuery();
                    }
                    hs1.Add(1);
                }

             
                combinationIds = new decimal[hs1.Count];
                hs1.CopyTo(combinationIds);
                for (i = 0; i < hs1.Count; i++)
                {
                    strcombinationIds += combinationIds[i] + ",";
                }
                strcombinationIds = strcombinationIds.Substring(0, strcombinationIds.Length - 1);
                queryString = "select combinationid from combination_criteria where combinationid in(" + strcombinationIds + ") group by combinationid having count(*)<=all(select count(*) from combination_criteria where combinationid in(" + strcombinationIds + ") group by combinationid)";
                cmd.CommandText = queryString;
                reader1 = cmd.ExecuteReader();
                reader1.Read();
                combinationId = (decimal)reader1["combinationid"];
                reader1.Close();
                queryString = "select occurences from disease_incidence where timeid=" + TimeId + " and disease='" + disease + "' and locationid=" + locId + " and combinationId =" + combinationId + "";
                cmd.CommandText = queryString;
                reader1 = cmd.ExecuteReader();
                if (reader1.HasRows)
                {
                    reader1.Close();
                    queryString = "update disease_incidence set occurences=occurences+1 where timeid=" + TimeId + " and disease='" + disease + "' and locationid=" + locId + " and combinationId =" + combinationId ;
                    cmd.CommandText = queryString;
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    reader1.Close();
                    queryString = "insert into disease_incidence values(" + locId + "," + TimeId + ",'" + disease + "'," + combinationId + ",1)";
                    cmd.CommandText = queryString;
                    cmd.ExecuteNonQuery();
                }


            }
            catch (SqlException e)
            {
                System.Console.WriteLine("SqlException in updateDiseaseIncidence:" + e);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
        }
        public static Guid getPatientHVId(string patientLoginId)
        {
            Guid pid = Guid.Empty;
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader reader1 = null;
            string queryString = "";
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                queryString = "select GlobalId from patient where patientId='" + patientLoginId + "'";
                cmd = new SqlCommand(queryString, con);
                reader1 = cmd.ExecuteReader();
                reader1.Read();
                pid = (Guid)reader1["Globalid"];
            }
            catch (SqlException e)
            {
                System.Console.Write(e);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
            return (pid);

        }
        public static bool isCaseActive(Guid patientId)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            bool boolStatus = false;
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                cmd = new SqlCommand("select patientId from patient_case,cases where cases.caseid=patient_case.caseid status<>"+(int)CASE_STATUS.CLOSED+"and pateintId='" + patientId + "'", con);
                if (cmd.ExecuteReader().HasRows)
                {
                    boolStatus = true;
                }

            }
            catch (SqlException e)
            {
                System.Console.WriteLine("Sqlexception in  isCaseActive:" + e);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
            return (boolStatus);
        }
        public static decimal getActiveCase(Guid patientId)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader reader1=null;
            decimal caseId = -1;
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                cmd = new SqlCommand("select cases.caseId from patient_case,cases where cases.caseid=patient_case.caseid and status<>" + (int)CASE_STATUS.CLOSED + " and patientId='" + patientId + "'", con);
                reader1=cmd.ExecuteReader();
                if (reader1.HasRows)
                {
                    reader1.Read();
                    caseId=(decimal)reader1["caseId"];
                }

            }
            catch (SqlException e)
            {
                System.Console.WriteLine("Sqlexception in  isCaseActive:" + e);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
            return (caseId);   
        }
        public static string findMedCentreId(decimal caseId)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader reader1 = null;
            string queryString = null;
            string medCentreId = null;
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                queryString = "select medcentreid from testcentre_medcentre where testcentreid=(select testcentreid from cases where caseid=" + caseId + " )";
                cmd = new SqlCommand(queryString, con);
                reader1 = cmd.ExecuteReader();
                if (reader1.HasRows)
                {
                    reader1.Read();
                    medCentreId = (string)reader1["medcentreid"];
                }
            }
            catch (SqlException e)
            {
                System.Console.WriteLine("SqlException in findMedCentreId:" + e);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
            return (medCentreId);
        }
        public static MedicalCase getCaseDetails(decimal caseId)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader reader1 = null;
            string queryString = null;
            MedicalCase mcase = null;
            Parameter[] parameters = null;
            DiseaseDiagonized[] diseases = null;
            Prescription[] prescriptions = null;
            int status = (int)CASE_STATUS.INSPECTION_PENDING;
            Guid patientId = new Guid();
            DateTime openDate = DateTime.MinValue;
            DateTime closeDate = DateTime.MaxValue;
            string testCentreId = null;
            int i, count;
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                queryString = "select * from Cases where caseid=" + caseId;
                cmd = new SqlCommand(queryString, con);
                reader1 = cmd.ExecuteReader();
                if (reader1.HasRows)
                {
                    reader1.Read();
                    openDate = (DateTime)reader1["opendate"];
                    closeDate = (DateTime)reader1["closedate"];
                    status = (int)reader1["status"];
                    testCentreId = (string)reader1["testcentreid"];
                }
                reader1.Close();

                queryString = "select parameterId,date_time,(select count(*) from parameter_case where caseid=" + caseId + ") as count from Parameter_case where caseid=" + caseId;
                cmd = new SqlCommand(queryString, con);
                reader1 = cmd.ExecuteReader();
                if (reader1.HasRows)
                {
                    i = 0;
                    reader1.Read();
                    count = (int)reader1["count"];
                    parameters = new Parameter[count];
                    do
                    {
                        parameters[i] = new Parameter(caseId, (int)reader1["parameterid"], (DateTime)reader1["Date_time"]);
                        i++;
                    } while (reader1.Read());
                }
                reader1.Close();

                queryString = "select doctorid,disease,date_time,(select count(*) from disease_case where caseid=" + caseId + ") as count from disease_case where caseId=" + caseId;
                cmd = new SqlCommand(queryString, con);
                reader1 = cmd.ExecuteReader();
                if (reader1.HasRows)
                {
                    i = 0;
                    reader1.Read();
                    count = (int)reader1["count"];
                    diseases = new DiseaseDiagonized[count];
                    do
                    {
                        diseases[i] = new DiseaseDiagonized((string)reader1["doctorid"], (string)reader1["disease"], (DateTime)reader1["date_time"]);
                        i++;
                    } while (reader1.Read());
                }
                reader1.Close();

                queryString = "select medicine,quantity,unit,instructions,doctorid,Date_time,(select count(*) from prescription where caseid=" + caseId + ") as count from prescription where caseId=" + caseId;
                cmd = new SqlCommand(queryString, con);
                reader1 = cmd.ExecuteReader();
                if (reader1.HasRows)
                {
                    i = 0;
                    reader1.Read();
                    count = (int)reader1["count"];
                    prescriptions = new Prescription[count];
                    do
                    {
                        prescriptions[i] = new Prescription(caseId, (string)reader1["medicine"], (int)reader1["quantity"], (string)reader1["unit"], (string)reader1["instructions"], (string)reader1["doctorId"], (DateTime)reader1["Date_time"]);
                        i++;
                    } while (reader1.Read());
                }
                reader1.Close();
                queryString = "select patientid from patient_case where caseid=" + caseId;
                cmd.CommandText = queryString;
                reader1 = cmd.ExecuteReader();
                if (reader1.HasRows)
                {
                    reader1.Read();
                    patientId = (Guid)reader1["patientid"];
                }
                reader1.Close();
                mcase = new MedicalCase(caseId, patientId, openDate, closeDate, status, testCentreId, prescriptions, diseases, parameters);
            }
            catch (SqlException e)
            {
                System.Console.WriteLine("SqlException in getCase:" + e);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
            return (mcase);
        }
        public static decimal createNewCase(Guid patientID, string testCentreId)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader reader1 = null;
            string queryString;
            DateTime openDate, closeDate;
            decimal caseId = -1;
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                caseId = generateCaseId();
                openDate = DateTime.Now;
                closeDate = DateTime.MaxValue;
                queryString = "select testcentreid from testcentre where testcentreid='" + testCentreId + "'";
                cmd = new SqlCommand(queryString, con);
                reader1 = cmd.ExecuteReader();
                if (reader1.HasRows)
                {
                    reader1.Close();
                    queryString = "insert into cases values(" + caseId + ",'" + openDate + "','" + closeDate + "'," + (int)CASE_STATUS.INSPECTION_PENDING + ",'" + testCentreId + "')";
                    cmd = new SqlCommand(queryString, con);
                    cmd.ExecuteNonQuery();
                    queryString = "insert into patient_case values('" + patientID + "'," + caseId + ")";
                    cmd.CommandText = queryString;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                System.Console.WriteLine("SqlException in createNewCase:" + e);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return (caseId);
        }
        public static decimal generateTimeId()
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader reader1 = null;
            string queryString;
            decimal tId = 1;
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                queryString = "select Timeid from Time order by timeid desc";
                cmd = new SqlCommand(queryString, con);
                reader1 = cmd.ExecuteReader();
                if (reader1.HasRows)
                {
                    reader1.Read();
                    tId = (decimal)reader1["timeid"] + 1;
                }
                reader1.Close();
            }
            catch (SqlException e)
            {
                System.Console.WriteLine("SqlException in generateTimeId:" + e);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return (tId);
        }
        public static decimal generateCaseId()
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader reader1 = null;
            string queryString;
            decimal caseId = 1;
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                queryString = "select caseid from cases order by caseid desc";
                cmd = new SqlCommand(queryString, con);
                reader1 = cmd.ExecuteReader();
                if (reader1.HasRows)
                {
                    reader1.Read();
                    caseId = (decimal)reader1["caseid"] + 1;
                }
                reader1.Close();
            }
            catch (SqlException e)
            {
                System.Console.WriteLine("SqlException in generateCaseId:" + e);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return (caseId);
        }
        public static DiseasePredicted[] predictDiseases(decimal caseId, string doctorId, DateTime dateTimeAllotted,HealthRecordInfo hri)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlCommand cmd2 = null;
            SqlDataReader reader1 = null;
            SqlDataReader reader2 = null;
            string queryString;
            decimal locId = 1;
            int[] criteriaId = null;
            decimal[] combinationIds = null;
            string strcombinationIds = "";
            string strTimeIds = "";
            int parameterId = -1;
            string parameterValue = null;
            Guid patientId = Guid.Empty;
            DiseasePredicted[] diseases = null;
            HashSet<decimal> hs1 = new HashSet<decimal>();
            HashSet<decimal> hs2 = new HashSet<decimal>();
            int i;
            decimal sumOfOccurences;
            decimal probability;
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                queryString = "select locationid from testcentre where testcentreid=" +
                    "(select testcentreid from cases where caseid=" + caseId + ")";
                cmd = new SqlCommand(queryString, con);
                reader1 = cmd.ExecuteReader();
                if (reader1.HasRows)
                {
                    reader1.Read();
                    locId = (decimal)reader1["locationid"];
                }
                reader1.Close();

                queryString = "select patientId from Patient_case where caseId=" + caseId;
                cmd.CommandText = queryString;
                reader1 = cmd.ExecuteReader();
                reader1.Read();
                patientId = (Guid)reader1["patientId"];
                reader1.Close();

                queryString = "select distinct parameterId,(select count(distinct parameterid) from parameter_case where caseid=" + caseId + ")as count  from Parameter_case where caseid=" + caseId;
                cmd = new SqlCommand(queryString, con);
                reader1 = cmd.ExecuteReader();
                if (reader1.HasRows)
                {
                    reader1.Read();
                    criteriaId = new int[(int)reader1["count"]];
                    i = 0;
                    do
                    {
                        parameterId = (int)reader1["parameterid"];
                        parameterValue = (new ParameterValues()).get_Parameter(parameterId, hri);
                        queryString = "select criteriaid from criteria where parameterid=" + parameterId + " and lowerbound<=" + parameterValue + " and upperbound>=" + parameterValue;
                        cmd2 = new SqlCommand(queryString, con);
                        reader2 = cmd2.ExecuteReader();
                        reader2.Read();
                        criteriaId[i] = (int)reader2["criteriaid"];
                        reader2.Close();
                        i++;
                    } while (reader1.Read());
                }
                reader1.Close();
                queryString = "select combinationId from combination";
                cmd.CommandText = queryString;
                reader1 = cmd.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        hs1.Add((decimal)reader1["combinationid"]);
                    }
                }
                reader1.Close();

                for (i = 0; criteriaId!=null && i < criteriaId.Length; i++)
                {
                    queryString = "select combinationid from combination_criteria where criteriaid=" + criteriaId[i];
                    cmd.CommandText = queryString;
                    reader1 = cmd.ExecuteReader();
                    while (reader1.Read())
                    {
                        hs2.Add((decimal)reader1["combinationid"]);
                    }
                    hs1.IntersectWith(hs2);
                    hs2.Clear();
                    reader1.Close();
                }
                combinationIds = new decimal[hs1.Count];
                hs1.CopyTo(combinationIds);
                for (i = 0; i < hs1.Count; i++)
                {
                    strcombinationIds += combinationIds[i] + ",";
                }
                strcombinationIds = strcombinationIds.Substring(0, strcombinationIds.Length - 1);
                queryString = "select Timeid from time where month=" + dateTimeAllotted.Month;
                cmd.CommandText = queryString;
                reader1 = cmd.ExecuteReader();
                while (reader1.Read())
                {
                    strTimeIds += (decimal )reader1["Timeid"] + ",";
                }
                strTimeIds = strTimeIds.Substring(0, strTimeIds.Length - 1);
                reader1.Close();
                queryString = "select sum(occurences)as Total from disease_incidence where timeid in(" +
                    strTimeIds + ") and combinationid in(" + strcombinationIds + ") and locationid=" + locId;
                cmd.CommandText = queryString;
                reader1 = cmd.ExecuteReader();
                reader1.Read();
                sumOfOccurences = (decimal)reader1["total"];
                reader1.Close();
                queryString = "select disease,sum(occurences)as Total,(" +
                    "select count(*) from disease_incidence where timeid in(" + 
                    strTimeIds + ") and combinationid in(" + 
                    strcombinationIds + ") and locationid=" +  locId  +
                ")as count from disease_incidence where timeid in(" + 
                strTimeIds + ") and combinationid in(" + strcombinationIds + ") and locationid=" + locId + " group by disease";
                cmd.CommandText = queryString;
                reader1 = cmd.ExecuteReader();
                reader1.Read();
                diseases = new DiseasePredicted[(int)reader1["count"]];
                i = 0;
                do
                {
                    probability = ((decimal)reader1["total"]) / sumOfOccurences;
                    diseases[i] = new DiseasePredicted((string)reader1["disease"], probability);
                    i++;
                } while (reader1.Read());
               
            }
            catch (SqlException e)
            {
                System.Console.WriteLine("SqlException in predictDisease:" + e);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return (diseases);
        }
        public static bool requestParameter(string parameterId, decimal caseId)
        {
            bool boolStatus = true;
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader reader1 = null;
            string queryString;
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                queryString = "select caseid from cases where caseid=" + caseId;
                cmd = new SqlCommand(queryString, con);
                reader1 = cmd.ExecuteReader();
                if (reader1.HasRows)
                {
                    reader1.Close();
                    queryString = "insert into parameter_request values(" + caseId + ",'" + parameterId + "','" + DateTime.Now + "')";
                    cmd.CommandText = queryString;
                    cmd.ExecuteNonQuery();
                }
                else
                    boolStatus = false;
            }
            catch (SqlException e)
            {
                System.Console.WriteLine("SqlException in requestParameter:" + e);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return (boolStatus);
        }
        public static PendingCases[] getPendingCases(string doctorId)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader reader1 = null;
            string querystring = null;
            PendingCases[] cases = null;
            int i;
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                querystring = "select caseid,doctorid,date_time_allotted,date_time_attended,attended_status,(select count(*) from doctor_case where doctorid='" + doctorId + "' and attended_status=0) as count from Doctor_case where doctorid='" + doctorId + "' and attended_status=0";
                cmd = new SqlCommand(querystring, con);
                reader1 = cmd.ExecuteReader();

                if (reader1.HasRows)
                {
                    i = 0;
                    reader1.Read();
                    cases = new PendingCases[(int)reader1["count"]];
                    do
                    {
                        cases[i] = new PendingCases((decimal)reader1["caseId"], (string)reader1["doctorid"], (DateTime)reader1["Date_time_allotted"], (DateTime)reader1["Date_time_attended"], (int)reader1["attended_status"]);
                        i++;
                    } while (reader1.Read());
                }
                reader1.Close();
            }
            catch (SqlException e)
            {
                System.Console.WriteLine("SqlException in getPendingcases:" + e);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
            return (cases);
        }
        public static bool addParameterToCase(int parameterId, decimal caseId)
        {
            bool boolStatus = true;
            SqlConnection con = null;
            SqlCommand cmd = null; ;
            SqlDataReader reader1 = null;
            string queryString = null;
            try
            {

                con = new SqlConnection(connectionString);
                con.Open();

                queryString = "select caseId from cases where caseId=" + caseId;
                cmd = new SqlCommand(queryString, con);
                reader1 = cmd.ExecuteReader();
                if (reader1.HasRows)
                {
                    reader1.Close();
                    queryString = "insert into parameter_case values(" + parameterId + "," + caseId + ",'" + DateTime.Now + "')";
                    cmd.CommandText = queryString;
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    reader1.Close();
                    boolStatus = false;
                }

            }
            catch (SqlException e)
            {
                System.Console.WriteLine("Sqlexception in addParameterToCase:" + e);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
            return (boolStatus);
        }
        public static string[] getDiseaseList()
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader reader1 = null;
            string[] diseases = null;
            int i;
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                cmd = new SqlCommand("select disease,(select count(*) from disease) as count from disease", con);
                reader1 = cmd.ExecuteReader();

                if (reader1.HasRows)
                {
                    reader1.Read();
                    diseases = new string[(int)reader1["count"]];
                    i = 0;
                    do
                    {
                        diseases[i] = (string)reader1["disease"];
                        i++;
                    } while (reader1.Read());

                }
            }
            catch (SqlException e)
            {
                System.Console.WriteLine("SqlException in getDiseaseList:" + e);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
            return (diseases);
        }
        public static decimal[] getPendingCasesTestcenter(string testCentreId)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader reader1 = null;
            string querystring = null;
            decimal[] cases = null;
            int i;
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                querystring = "select caseid,(select caseid from cases where status<>" + (int)Operations.CASE_STATUS.CLOSED + " and testcentreid='" + testCentreId + "') as count from cases where status<>" + (int)Operations.CASE_STATUS.CLOSED + " and testcentreid='" + testCentreId + "'";
                cmd = new SqlCommand(querystring, con);
                reader1 = cmd.ExecuteReader();

                if (reader1.HasRows)
                {
                    i = 0;
                    reader1.Read();
                    cases = new decimal[Int32.Parse(reader1["count"].ToString())];
                    do
                    {
                        cases[i] = (decimal)reader1["caseid"];
                        i++;
                    } while (reader1.Read());
                }
                reader1.Close();
            }
            catch (SqlException e)
            {
                System.Console.WriteLine("SqlException in getPendingcasesTestcentre:" + e);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
            return (cases);
        }
        public static List<decimal> getPastCases(Guid PatientId)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader reader1 = null;
            
            List<decimal> l = new List<decimal>();
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                cmd = new SqlCommand("select cases.caseId from cases,patient_case where cases.caseid=patient_case.caseid and status="+(int)CASE_STATUS.CLOSED+" and patientId='"+PatientId+"' order by opendate desc", con);
                reader1 = cmd.ExecuteReader();
                while (reader1.Read())
                {
                    l.Add((decimal)reader1["caseid"]);
                }
                
            }
            catch (SqlException e)
            {
                System.Console.WriteLine("SqlException in getPastCases:" + e);
            }
            finally
            {
                if (con != null)
                    con.Close();
            }
            return (l);
        }
    }
    class QuestionAnswer
    {
        public  string question, answer;
        public int status;
        public decimal caseId, questionId;
        public DateTime qAsked;
        public QuestionAnswer(string question,string answer,decimal caseId,decimal questionId,int status,DateTime qAsked) 
        {
            this.answer = answer;
            this.question = question;
            this.status = status;
            this.caseId = caseId;
            this.questionId = questionId;
            this.qAsked = qAsked;
        }
    }
    class Prescription
    {
        public string medicine;
        public decimal caseId;
        public DateTime prescriptionDateTime;
        public int quantity;
        public string unit;
        public string instructions;
        public string doctorId;
        //public string medCentreId;
        public Prescription(decimal caseId, string medicine, int quantity, string unit, string instructions, string doctorId, DateTime prescriptionDateTime)
        {
            this.caseId = caseId;
            this.medicine = medicine;
            this.quantity = quantity;
            this.unit = unit;
            this.instructions = instructions;
            this.doctorId = doctorId;
            this.prescriptionDateTime = prescriptionDateTime;
        }

    }
    class DiseaseDiagonized
    {
        public string doctorId;
        public string disease;
        public DateTime diagonsisDateTime;
        public DiseaseDiagonized(string doctorId, string disease, DateTime diagonsisDateTime)
        {
            this.doctorId = doctorId;
            this.disease = disease;
            this.diagonsisDateTime = diagonsisDateTime;
        }
    }
    class Parameter
    {
        public decimal caseId;
        public DateTime parameterInputDateTime;
        public int parameterId;
        public Parameter(decimal caseId, int parameterId, DateTime parameterInputDateTime)
        {
            this.caseId = caseId;
            this.parameterInputDateTime = parameterInputDateTime;
            this.parameterId = parameterId;
        }
    }
    class MedicalCase
    {
        public decimal caseId;
        public Guid patientId;
        public DateTime openDate, closeDate;
        public int status;
        public Prescription[] prescriptions;
        public DiseaseDiagonized[] diseases;
        public Parameter[] associatedParameters;
        public string testCentreId;
        public MedicalCase(decimal caseId, Guid patientId, DateTime openDate, DateTime closeDate, int status, string testCentreId, Prescription[] p, DiseaseDiagonized[] d, Parameter[] parameters)
        {
            this.caseId = caseId;
            this.openDate = openDate;
            this.closeDate = closeDate;
            this.status = status;
            this.prescriptions = p;
            this.diseases = d;
            this.associatedParameters = parameters;
            this.testCentreId = testCentreId;
            this.patientId = patientId;
        }
        public override string ToString()
        {
            string s = "";
            s += "Case Id:" + caseId;
            s += "\nPatient Id:" + patientId;
            s += "\nOpen date:" + openDate;
            if (status == (int)Operations.CASE_STATUS.CLOSED)
                s += "\nClose date:" + closeDate;
            s += "\nStatus:" + getStatus();
            s += "\nTest centre:" + testCentreId;
            for (int i = 0; i < prescriptions.Length; i++)
            {
                s += "\nPrescription " + i + ":" + prescriptions[i].medicine + " Quantity:" + prescriptions[i].quantity + prescriptions[i].unit + " Instructions:" +
                    prescriptions[i].instructions + " DoctorId" + prescriptions[i].doctorId + " DateTime:" + prescriptions[i].prescriptionDateTime;
            }
            for (int i = 0; i < diseases.Length; i++)
            {
                s += "\nDisease " + i + ":" + diseases[i].disease + " DoctorId" + diseases[i].doctorId + " DateTime:" + diseases[i].diagonsisDateTime;
            }
            return (s);
        }
        public string getStatus()
        {
            switch (status)
            {
                case (int)Operations.CASE_STATUS.CLOSED:
                    return ("Closed");
                case (int)Operations.CASE_STATUS.INSPECTION_PENDING:
                    return ("Inspection Pending");
                case (int)Operations.CASE_STATUS.PARAMETER_REQUESTED:
                    return ("Parameter Requested");
                case (int)Operations.CASE_STATUS.PARAMETER_REQUESTED_PRESCRIPTION_GIVEN:
                    return ("Parameter Requested and Prescription given");
                case (int)Operations.CASE_STATUS.PRESCRIPTION_GIVEN:
                    return ("Prescription Given");
                default:
                    return ("");
            }

        }


    }
    class Location
    {
        public string street;
        public string village_town_city;
        public string district;
        public string state;
        public Location(string street, string village_town_city, string district, string state)
        {
            this.street = street;
            this.village_town_city = village_town_city;
            this.district = district;
            this.state = state;
        }
        public Location() { }
    }
    class DiseasePredicted
    {
        public string disease;
        public decimal probability;
        public DiseasePredicted(string disease, decimal probability)
        {
            this.disease = disease;
            this.probability = probability;
        }
    }
    class PendingCases
    {
        public decimal caseId;
        public string doctorId;
        public DateTime dateTimeAttended;
        public DateTime dateTimeAllotted;
        int status;
        public PendingCases(decimal caseId, string doctorId, DateTime dateTimeAllotted, DateTime dateTimeAttended, int status)
        {
            this.caseId = caseId;
            this.doctorId = doctorId;
            this.dateTimeAllotted = dateTimeAllotted;
            this.dateTimeAttended = dateTimeAttended;
            this.status = status;
        }
    }
}


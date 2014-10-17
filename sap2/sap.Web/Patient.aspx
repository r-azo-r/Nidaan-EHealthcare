<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Patient.aspx.cs" Inherits="sap.Web.Patient" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
`<head runat="server">
    <title>Patient Page</title>
     <style type="text/css">
               #Top
        {
            width:100%;
            height:120px;
            background:#070842;
            float: left;
            font-family:"Copperplate Gothic Bold";
            font-size:50px;
            text-align:center;
            vertical-align:middle;
            color:Maroon;
        }
        #LeftPanel
        {
            width: 23%;
            float: left;
        
        }
        #CentrePanel
        {
            width: 75%;
            margin-left:12.5%;
            float:left;
        
        }
       .width100{width:100%;}
      .tableheading1{background-color: Maroon;color:White;font-family:Copperplate Gothic Bold;font-size:20px;}
      .tableheading2{background-color:  #eeeeee ;color:Black;font-family:Copperplate Gothic Bold;font-size:15px;text-align:center;}
      .tablerow1{background-color:  #eeeeee;font-family:" Arial Black";font-size:15px;text-align:center;}
      .tab{width:13.5%}
      
    </style>
        <script type="text/javascript" src="js/jquery-1.3.2.min.js"></script>
		<script type="text/javascript" src="js/jquery-ui-1.7.2.custom.min.js"></script>
         <script type="text/javascript" >
             $(function () {
                
            // Tabs
            $('#tabs').tabs();
        });
      
    </script>
     <link type="text/css" href="css/custom-theme/jquery-ui-1.7.2.custom.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="Top">
     <img src="Images/Logo_Final.png"></img>
    </div>
    <div id="LeftPanel">
        
    </div>
    <div id="CentrePanel">
    
     <div id="tabs">
			<ul>
			    <li class="tab"><a href="#basicInformationTab">Basic<br />Information</a></li>
			    <li class="tab"><a href="#addDetailsTab">Add Medical<br />Parameters</a></li>
			    <li class="tab"><a href="#medicalDetailsTab">Medical<br />Details</a></li>
                <li class="tab"><a href="#DiagnosisTab">Diagnosis</a></li>
                <li class="tab"><a href="#askQuestionsTab">Prescribed<br />Medicine</a></li>
			</ul>
			
			<div id="basicInformationTab" runat="server">
       
            <b>Basic Information</b>
  
   <table border=1>
       
       <tr>
        <td>Name</td>
        <td>
            <asp:Label ID="PersonName" runat="server"></asp:Label>
           </td>
       </tr>
      
       <tr>
         <td>Age</td>
         <td>
             <asp:Label ID="PersonAge" runat="server"></asp:Label>
           </td>
       </tr>
       
       <tr>
        <td>Gender</Td>
        <td>
            <asp:Label ID="PersonGender" runat="server"></asp:Label>
           </td>
       </tr>
       
       <tr>
         <td >Religion</td>
         <td> 
             <asp:Label ID="PersonReligion" runat="server"></asp:Label>
           </td>
       </tr>
                    <tr>
                        <td >
                            Blood Group</td>
                        <td>
                            <asp:Label ID="PersonBloodGroup" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            Diet</td>
                        <td>
                            <asp:Label ID="PersonDiet" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td >
                            Marital Status</td>
                        <td>
                            <asp:Label ID="PersonMarital" runat="server"></asp:Label>
                        </td>
                    </tr>
   
   </table>
   </div>
             <div id="addDetailsTab" runat="server">
             
             <table>
             <tr>
             <td>Weight(in Kg)</td>
             <td><asp:TextBox ID="Weight" runat="server"></asp:TextBox></td>
             </tr>
             <tr>
             <td></td>
             <td><asp:Button ID="wtbtn" 
                     runat="server" Text="Add" onclick="wtbtn_Click" ></asp:Button></td>
             </tr>
             
             <tr>
             <td>Height(in metres)</td>
             <td>
             <asp:TextBox ID="Height" runat="server" > </asp:TextBox>
             </td>
             <tr>
             <td></td>
             <td>
            <asp:Button ID="htbtn" 
                     runat="server" Text="Add" onclick="htbtn_Click" ></asp:Button></td>
             </tr>
             
             <tr>
             <td>Temperature(°C)</td>
             
             <td><asp:TextBox ID="Temperature" runat="server"></asp:TextBox></td>
             <tr>
             <td></td>
             <td>
                 <asp:Button ID="tempbtn" runat="server" Text="Add" onclick="tempbtn_Click" ></asp:Button></td>
             </tr>
             
             <tr>
             <td>Blood Pressure(mm of Hg)</td>
             </tr>
             <tr>
             <td>Systolic</td>
             <td><asp:TextBox ID="BloodPressure1" runat="server"></asp:TextBox></td>
             </tr>
             <tr>
             <td>Diastolic</td>
             <td><asp:TextBox ID="BloodPressure2" runat="server"></asp:TextBox></td>
             </tr>
             <tr>
             <td></td>
                 <td><asp:Button ID="bpbtn" runat="server" Text="Add" onclick="bpbtn_Click" ></asp:Button></td>
             </tr>
             <tr>
             <td>Haemoglobin(gm/dL)</td>
             <td><asp:TextBox ID="Haemoglobin" runat="server"></asp:TextBox></td>
             <tr>
             <td></td>
             <td>
                 <asp:Button ID="haembtn" runat="server" Text="Add" onclick="haembtn_Click" ></asp:Button></td>
             </tr>
             <tr>
             <td>Blood Glucose(mg/dL)</td>
             <td><asp:TextBox ID="BloodGlucose1" runat="server"></asp:TextBox></td>
             <tr>
             <td></td>
             <td>
                 <asp:Button ID="bgbtn" runat="server" Text="Add" onclick="bgbtn_Click" ></asp:Button></td>
             </tr>
              
              <tr>
             <td>Cholesterol(mg/dL)</td>
             </tr>
             <tr>
             <td>HDL</td>
             <td><asp:TextBox ID="Cholesterol1" runat="server"></asp:TextBox></td>
             </tr>
             <tr>
             <td>LDL</td>
             <td><asp:TextBox ID="Cholesterol2" runat="server"></asp:TextBox></td>
             </tr>
             <tr>
             <td>Triglyceride</td>
             <td>
             <asp:TextBox ID="Cholesterol3" runat="server"></asp:TextBox></td>
             </tr>
             <tr>
             <td></td>
             <td><asp:Button ID="cholesbtn" runat="server" Text="Add" onclick="cholesbtn_Click" ></asp:Button></td>

             </tr>
             
             </table>
             </div>
                   <div id="medicalDetailsTab" runat="server">
             <table >
             
             <tr>
             <td>
             Weight(in kg)
             </td>
             <td>
                 <asp:Label ID="Weightlbl" runat="server"></asp:Label>
             </td>
             </tr>
             
             
             <tr>
             <td>Temperature(°C)
             </td>
             <td>
                 <asp:Label ID="Templbl" runat="server"></asp:Label>
             </td>
             </tr>
             
             <tr>
             <td>Blood Glucose(gm/dL)
             </td>
             <td>
                 <asp:Label ID="Bglbl" runat="server"></asp:Label>
             </td>
             </tr>
             
             <tr>
             <td>Haemoglobin(gm/dL)
             </td>
             <td>
                 <asp:Label ID="Haemoglobinlbl" runat="server"></asp:Label>
             </td>
             </tr>
             
             
             <tr>
             <td>Blood Pressure(mm of Hg)
             </td>
             <td>
                 <asp:Label ID="Bplbl" runat="server"></asp:Label>
             
             </td>
             </tr>
             
             
             <tr>
             <td>Cholesterol(mg/dL)
             </td>
             <td>
             <asp:Label ID="Cholesterollbl" runat="server"></asp:Label>
             
             </td>
             </tr>
             </table>
             </div>
    </div>
    </div>
    </form>
</body>
</html>

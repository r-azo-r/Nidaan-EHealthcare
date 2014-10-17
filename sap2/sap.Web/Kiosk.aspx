<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Kiosk.aspx.cs" Inherits="sap.Web.Kiosk" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Kiosk Page</title>
    <script type="text/javascript" src="js/jquery-1.3.2.min.js"></script>
		<script type="text/javascript" src="js/jquery-ui-1.7.2.custom.min.js"></script>
    <script type="text/javascript" >
    		$(function(){

				// Accordion
				$("#accordion").accordion({ header: "h3" });
				
				// Tabs
				$('#tabs').tabs();
				});
    </script>
     <link type="text/css" href="css/custom-theme/jquery-ui-1.7.2.custom.css" rel="stylesheet" />	
    <style type="text/css">
         #Top{
            width:100%;
            background:#070842;
            height:120px;
            float :left;
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
            float: left; 
            
        }
        .table1{width:100%}
        .tablecell1{ padding-left:0px;padding-right:0px;}
        .tablerow1{background-color:#eeeeee}
        .tab{width:19%}
    </style>
</head>
<body>
<form id="form1" runat="server">
    <div id="Top">
     <img src="Images/Logo_Final.png"></img>
    </div>
   
    <div id="CentrePanel">
    <table ID="Table1" runat="server" class="table1">
     <tr class="tablerow1">
      <td class="tablecell1" style="width:40%">
       Current Patient:    
        <asp:Label ID="patientName" runat="server"></asp:Label>
      </td>
      <td class="tablecell1">
        <asp:DropDownList ID="DropDownList1" runat="server" onselectedindexchanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
         <asp:ListItem>--Select a Patient--</asp:ListItem>
         </asp:DropDownList>
          <asp:Button id="btnCreateNewCase" runat="server" Text="Create New Case" Enabled="False" onclick="btnCreateNewCase_Click"/>
          <asp:Button ID="btnSendCaseToDoctor" runat="server" Text="Send Case To Doctor" onclick="btnSendCaseToDoctor_Click"></asp:Button>
          <a href="https://account.healthvault-ppe.com/default.aspx" target="_Blank">Go To Health Vault</a>
          
       </td>
      </tr>
    </table>
    
    <div id="tabs">
    
     <ul>
      <li class="tab"><a href="#QuestionsTab">Questions<br/>&nbsp;</a></li>
      <li class="tab"><a href="#DiseasesDiagonizedTab">Diseases<br/> Diagonized</a></li>
      <li class="tab"><a href="#MedicinesPrescribedTab">Medicines<br/> Prescribed</a></li>
      <li class="tab" ><a href="#SymptomsTab">Symptoms/<br/>Discomfort</a></li>
      <li class="tab" ><a href="#ParametersTab">Parameters<br/>&nbsp;</a></li>
     </ul>
     <div id="QuestionsTab" runat="server">
      <asp:Button ID="btnAnswer" runat="server" Text="Send Asnswers to Doctor"  OnClick="btnAnswer_Click" UseSubmitBehavior="True" Visible="False" />
     </div>
     <div id="DiseasesDiagonizedTab" runat="server">
     </div>
     <div id="MedicinesPrescribedTab" runat="server">
     </div>
     <div id="SymptomsTab" runat="server">
     </div>
      <div id="ParametersTab" runat="server">
      <div class="ui-widget">
			<div class="ui-state-highlight ui-corner-all" style="margin-top: 20px; padding: 0 .7em;"> 
				<p><span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span>
				Parameters entered in the HealthVault within last 24 hours are shown below.</p>
			</div>
		</div>
        <br />
        <table style="width:70%;" cellspacing="5px" cellpadding="5px">
         <tr>
          <td>Temperature         
          </td>
          <td><asp:Label id="lblTemperature" runat="server"></asp:Label>
          </td>
         </tr>
         <tr>
          <td>Weight         
          </td>
          <td><asp:Label id="lblWeight" runat="server"></asp:Label>
          </td>
         </tr>
         <tr>
          <td>Blood Glucose         
          </td>
          <td><asp:Label id="lblBloodSugar" runat="server"></asp:Label>
          </td>
         </tr>
         <tr>
          <td>Blood Pressure
          </td>
          <td><asp:Label id="lblBloodPressure" runat="server"></asp:Label>
          </td>
         </tr>
         <tr>
          <td>Haemoglobin         
          </td>
          <td><asp:Label id="lblHaemoglobin" runat="server"></asp:Label>
          </td>
         </tr>
         <tr>
          <td>Cholesterol   
          </td>
          <td><asp:Label id="lblCholesterol" runat="server"></asp:Label>
          </td>
         </tr>
         <tr>
          <td colspan="2">
            <asp:Button ID="btnAddParametersToCase" runat="server" Text="Add Parameters To Case" OnClick="btnAddParametersToCase_Click"></asp:Button>
          </td>
        </tr>
        </table>
     </div>
    </div>
    </div>
    </form>
</body>
</html>

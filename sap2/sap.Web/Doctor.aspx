<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Doctor.aspx.cs" Inherits="sap.Web.Doctor" %><%@ Register Assembly="DundasWebChart" Namespace="Dundas.Charting.WebControl" TagPrefix="DCWC" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
	
       <script type="text/javascript" src="js/jquery-1.3.2.min.js"></script>
		<script type="text/javascript" src="js/jquery-ui-1.7.2.custom.min.js"></script>
    <script type="text/javascript" >
        $(function () {

            // Accordion
            $("#accordion").accordion({ header: "h3" });
            $("#accordion1").accordion({header:"h4"});
            // Tabs
            $("#tabs").tabs();

        });
      
    </script>
     <link type="text/css" href="css/custom-theme/jquery-ui-1.7.2.custom.css" rel="stylesheet" />	
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
			    <li class="tab"><a href="#pendingCasesTab" >Pending<br />Cases</a></li>
				<li class="tab"><a href="#basicInformationTab">Basic<br />Information</a></li>
				<li class="tab"><a href="#medicalDetailsTab">Medical<br />Details</a></li>
				
                <li class="tab"><a href="#diagonizeDiseaseTab">Diagonize<br />Disease</a></li>
				<li class="tab"><a href="#prescribeMedicineTab">Prescribe<br />Medicine</a></li>
                <li class="tab"><a href="#pastCasesTab">Past<br />Cases</a></li>
                 <li class="tab"><a href="#askQuestionsTab">Ask<br />Questions</a></li>
			</ul>
			<div id="pendingCasesTab" runat="server">
			 <asp:Table ID="tblPendingCases" runat="server" CssClass="width100">
              <asp:TableHeaderRow CssClass="tableheading2"><asp:TableCell>Case Id</asp:TableCell><asp:TableCell>Case Allocation Date Time </asp:TableCell></asp:TableHeaderRow>
             </asp:Table>
            </div>
			<div id="basicInformationTab" runat="server">
            </div>
			<div id="medicalDetailsTab" runat="server">
			 <div id="accordion" style="font-family:Arial">
			  <div>
				<h3><a href="#">Temperature</a></h3>
				<div id="temperatureData" runat="server"></div>
			 </div>
			 <div>
				<h3><a href="#">Weight</a></h3>
				<div id="weightData" runat="server"></div>
			 </div>
			 <div>
				<h3><a href="#">Blood Pressure</a></h3>
				<div id="bloodPressureData" runat="server"></div>
			 </div>
			 <div>
				<h3><a href="#">Blood Glucose</a></h3>
				<div id="bloodGlucoseData" runat="server"></div>
			 </div>
			 <div>
				<h3><a href="#">Haemoglobin</a></h3>
				<div id="haemoglobinData" runat="server"></div>
		     </div>
			 <div>
				<h3><a href="#">Cholesterol</a></h3>
				<div id="cholesterolData" runat="server"></div>
			 </div>
			 <div>
				<h3><a href="#">Allergies</a></h3>
				<div id="allergyData" runat="server"></div>
			 </div>
             <div>
				<h3><a href="#">Symptoms</a></h3>
				<div id="symptomsData" runat="server"></div>
		  	 </div>
			 <div>
				<h3><a href="#">Diseases Diagonized</a></h3>
				<div id="diseasesDiagonized" runat="server"></div>
			 </div>
			 <div>
				<h3><a href="#">Prescription given</a></h3>
				<div id="prescriptionGiven" runat="server"></div>
			 </div>
			 </div>
			</div>
        
			<div id="diagonizeDiseaseTab" >
			<asp:Table ID="Table1" runat="server">
			<asp:TableRow>
             <asp:TableCell><asp:ListBox ID="DiseaseListBox" runat="server" Width="300px"></asp:ListBox></asp:TableCell> 
             <asp:TableCell>
              <asp:ListBox id="parameterList" runat="server"  Width="300px">
               <asp:ListItem>Temperature</asp:ListItem>
               <asp:ListItem>Blood Pressure</asp:ListItem>
               <asp:ListItem>Blood Glucose</asp:ListItem>
               <asp:ListItem>Weight</asp:ListItem>
              </asp:ListBox>
             </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
             <asp:TableCell HorizontalAlign="Center"><asp:Button ID="btnDiagonizeDisease" runat="server" Text="Diagonize Disease" 
                    onclick="btnDiagonizeDisease_Click" /></asp:TableCell>
             <asp:TableCell HorizontalAlign="Center"> <asp:Button ID="btnRequestParameter" runat="server" Text="Request Parameter" onclick="btnRequestParameter_Click"/></asp:TableCell>
            </asp:TableRow>
            </asp:Table>
            <asp:Label id="lblPredictedDisease" Text="Suggested Diseases:" runat="server"></asp:Label>
			</div>
			<div id="prescribeMedicineTab" runat="server">
                <asp:Table ID="Table2" runat="server">
                <asp:TableRow>
                <asp:TableCell><asp:Label ID="lblMedicineName" runat="server" Text="Medicine Name"></asp:Label></asp:TableCell>
                <asp:TableCell><asp:TextBox ID="txtMedicineName" runat="server" Width="170px"></asp:TextBox></asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                   <asp:TableCell>
                     <asp:Label ID="lblquantity" runat="server" Text="Quantity"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                      <asp:TextBox ID="txtQuantity" runat="server" Width="50px"></asp:TextBox>
                       &nbsp;<asp:Label ID="lblunit" runat="server" Text="Unit"></asp:Label>
                      <asp:TextBox ID="txtUnit" runat="server" Width="30px"></asp:TextBox>
                    </asp:TableCell>
                
                </asp:TableRow>
                <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="lblInstructions" runat="server" Text="Instructions"></asp:Label>
                 </asp:TableCell>
                    <asp:TableCell>
                        <asp:TextBox ID="txtInstructions" runat="server" TextMode="MultiLine" Rows="10" Width="170px" MaxLength="3000"></asp:TextBox></asp:TableCell>
                </asp:TableRow>
                
                  
                <asp:TableRow>
                  <asp:TableCell><asp:Button runat="server" Text="Prescribe" ID="btnPrescribeMedicine" OnClick="btnPrescribeMedicine_Click"/> </asp:TableCell>
                  <asp:TableCell><asp:Button runat="server" Text="Close Case" ID="btnCloseCase" OnClick="btnCloseCase_Click" /></asp:TableCell>
                 <asp:TableCell><asp:Button runat="server" Text="Send SMS" ID="btnSMS" OnClick="btnSMS_Click" /></asp:TableCell>
                  </asp:TableRow>
                  <asp:TableRow><asp:TableCell>
                  
                 
                   <div class="ui-widget">
			<div id="smsnote" class="ui-state-highlight ui-corner-all"  runat="server"> 
				<p style="font-size:10px;height:7px"><span class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span>
				SMS has been succesfully sent.</p>
			</div>
		</div></asp:TableCell></asp:TableRow>
                </asp:Table>
                
			    </div>
			<div id="pastCasesTab" runat="server">
             <div id="accordion1" runat="server">
             </div>
            </div>
            <div id="askQuestionsTab" runat="server">
             <table class="width100" border="0" ID="tblQuestions" runat="server">
              <tr>
               <td style="width:40%">
                <asp:ListBox ID="list1" runat="server" CssClass="width100" AutoPostBack="False"></asp:ListBox>
               </td>
               <td valign="middle" align="center">
                 <asp:Button Text="Add >" id="btnAddQuestion" onClick="btnAddQuestion_Click" runat="server" />
               </td>
               <td  style="width:40%">
                  <asp:ListBox ID="list2" runat="server" CssClass="width100" AutoPostBack="True"></asp:ListBox>
               </td>
              </tr>
              <tr>
               <td colspan ="2"> Add Your Own Question<asp:TextBox Id="txtQuestion" runat="server"   style="width:100%"></asp:TextBox></td>
               <td align="center" valign="bottom"><asp:Button id="btnAddOwnQuestion" runat="server" Text="Add Question" OnClick="btnAddOwnQuestion_Click"  style="width:60%"/></td>
              </tr>
              <tr>
               <td colspan="3"><asp:Button id="btnSendQuestionsToPatient" 
                       Text="Send Questions to Patient" OnClick="btnSendQuestionToPatient_Click" 
                       runat="server" /></td>
              </tr>
              <tr>
               <td colspan="3">Past Q&A</td>
              </tr>
               <tr>
               <th align="left">Question</th>
               <th align="left">Answer</th>
               <th align="left">Date Time</th>
              </tr>
             </table>
            </div>
		</div>
   
    
    </form>
        
</body>
</html>

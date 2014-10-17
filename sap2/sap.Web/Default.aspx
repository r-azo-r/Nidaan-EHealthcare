<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="sap.Web._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        .style2
        {
            width: 187px;
        }
        .style3
        {
            width: 190px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="Title">
     <h2>
        <asp:Label ID="Label1" runat="server" Text="Doctor Name"></asp:Label>
     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Home.aspx">Home</asp:HyperLink>
     </h2>
     <div id="Main1">
       <b>Basic Information</b>
                <table class="style1">
                    <tr>
                        <td class="style2">
                            Name</td>
                        <td>
                            <asp:Label ID="PersonName" runat="server"></asp:Label>
                            <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Button" 
                                BackColor="White" BorderColor="#66CCFF" BorderStyle="Double" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Age</td>
                        <td>
                            <asp:Label ID="PersonAge" runat="server" Text="Label"></asp:Label>
                            <asp:Image ID="Image1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Sex</td>
                        <td>
                            <asp:Label ID="PersonSex" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Religion</td>
                        <td>
                            <asp:Label ID="PersonReligion" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Occupation</td>
                        <td>
                            <asp:Label ID="PersonOccupation" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Blood Group</td>
                        <td>
                            <asp:Label ID="PersonBloodGroup" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Diet</td>
                        <td>
                            <asp:Label ID="PersonDiet" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Marital Status</td>
                        <td>
                            <asp:Label ID="PersonMarital" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Family Information</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            <hr />
            <b>Allergies<br />
         </b>
&nbsp;<asp:Label ID="NoAllergy" runat="server" 
             Text="No Allergies to Show" Visible="False"></asp:Label>
         <asp:Table ID="Table1" runat="server" BorderColor="Black" BorderStyle="Solid" 
             BorderWidth="1px" CellPadding="1" CellSpacing="1">
         </asp:Table>
         <hr />
            <b>Personal Habits</b>
               <table>
               <tr>
               <td class="style3">Alcohol Consumption</td>
               <td style="margin-left: 160px">
                   <asp:Label ID="PersonAlcohol" runat="server" Text="Label"></asp:Label>
                   </td>
               </tr>
               <tr>
               <td class="style3">Smoking</td>
               <td style="margin-left: 200px">
                   <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                   </td>
               </tr>
               </table>
            
           <hr />
           
     </div>
     <hr />
      </div>
     <div id="silverlightControlHost" height="300" width="300">
        <object data="data:application/x-silverlight-2," 
             type="application/x-silverlight-2" width="100%" style="height: 217px">
		  <param name="source" value="ClientBin/sap.xap"/>
		  <param name="onError" value="onSilverlightError" />
		  <param name="background" value="white" />
		  <param name="minRuntimeVersion" value="3.0.40818.0" />
		  <param name="autoUpgrade" value="true" />
		  <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=3.0.40818.0" style="text-decoration:none">
 			  <img src="http://go.microsoft.com/fwlink/?LinkId=108181" alt="Get Microsoft Silverlight" style="border-style:none"/>
		  </a>
	    </object><iframe id="_sl_historyFrame" style="visibility:hidden;height:0px;width:0px;border:0px"></iframe></div>
	    <div id="Main2">
        <hr />
            <b>Medical Parameters</b><br />
            <asp:DropDownList ID="DropDownList1" runat="server">
                <asp:ListItem>Temperature</asp:ListItem>
                <asp:ListItem>Weight</asp:ListItem>
                <asp:ListItem>Blood Pressure Systolic</asp:ListItem>
                <asp:ListItem>Blood Pressure Diastolic</asp:ListItem>
                <asp:ListItem>Blood Sugar Level (Before Eating)</asp:ListItem>
                <asp:ListItem>Blood Sugar Level (After Eating)</asp:ListItem>
                <asp:ListItem>Haemoglobin</asp:ListItem>
                <asp:ListItem>Blood Glucose Levels</asp:ListItem>
                <asp:ListItem>High Density Lipid</asp:ListItem>
                <asp:ListItem>Low Density Lipid</asp:ListItem>
                <asp:ListItem>Triglyceride</asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="Button" />
            <br />
            <asp:MultiView ID="MultiView1" runat="server" 
                onactiveviewchanged="MultiView1_ActiveViewChanged">
                <asp:View ID="View1" runat="server">
                    temperature
                </asp:View>
                <asp:View ID="View2" runat="server">
                    Hello world
                </asp:View>
                <asp:View ID="View3" runat="server">
                    Blood Pressure
                </asp:View>
                <asp:View ID="View4" runat="server">
                    Weight
                </asp:View>
            </asp:MultiView>
&nbsp;<hr />
          DSS Output
         <hr />
            Doctors Opinion & Request for More Parameters
         </div>
    <p>
        <asp:Button ID="OptionalAuth"
            runat="server" Text="optional auth" onclick="Button1_Click" />
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        </p>
	     </form>
</body>
</html>

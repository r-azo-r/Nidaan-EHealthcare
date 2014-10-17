<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HealthOfficial.aspx.cs" Inherits="sap.Web.HealthOfficial" %><%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>



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

            // Tabs
            $('#tabs').tabs();
        });
    </script>
     <link type="text/css" href="css/ui-lightness/jquery-ui-1.7.2.custom.css" rel="stylesheet" />	
    <style type="text/css">
        #Top
        {
            width:100%;
            height:118px;
            background:#070842;
            float: left;
            font-family:"Copperplate Gothic Bold";
            font-size:50px;
            text-align:center;
            vertical-align:middle;           
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
      .table1{width:100%;}
      .tableheading1{background-color: Maroon;color:White;font-family:Copperplate Gothic Bold;font-size:20px;}
      .tableheading2{background-color:  #eeeeee ;color:Black;font-family:Copperplate Gothic Bold;font-size:15px;text-align:center;}
      .tablerow1{background-color:  #eeeeee;font-family:" Arial Black";font-size:15px;text-align:center;}
      .tab{width:16%}
        .style1
        {
            font-size: xx-large;
            color: #28611F;
        }
        .style2
        {
            color: #215F28;
        }
        </style>
</head>
<body style="height: 116px; width: 1187px">

    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">  
              
                 </asp:ScriptManager>
                   
                 
                <div id="Top">
         <img src="Images/Logo_Final.png"></img>
       
    </div>
    
    <div id="LeftPanel">
        
    </div>
    <div id="CentrePanel">
    
		
			
			<div id="accordion">
			<div>
				<h3><a href="#">ByArea </a></h3>             
                	    
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:AKAV1ConnectionString1 %>" 
                        
                    
                    SelectCommand="SELECT [Street], [Village_town_city], [District], [State] FROM [location]">
                    </asp:SqlDataSource>
                   <div>
                    <asp:DropDownList ID="DropDownList1" runat="server" BackColor="White" 
                        DataSourceID="SqlDataSource1" DataTextField="Street" DataValueField="Street" 
                        Font-Names="Verdana" Font-Size="Small" Height="25px" Width="149px">
                    </asp:DropDownList>
                    <asp:DropDownList ID="DropDownList2" runat="server" BackColor="White" 
                        DataSourceID="SqlDataSource1" DataTextField="Village_town_city" 
                        DataValueField="Village_town_city" Font-Names="Verdana" Font-Size="Small" 
                        Height="25px" Width="145px">
                    </asp:DropDownList>
                    <asp:DropDownList ID="DropDownList3" runat="server" BackColor="White" 
                        DataSourceID="SqlDataSource1" DataTextField="District" 
                        DataValueField="District" Font-Names="Verdana" Font-Size="Small" Height="25px" 
                        Width="153px">
                    </asp:DropDownList>
                    <asp:DropDownList ID="DropDownList4" runat="server" BackColor="White" 
                        DataSourceID="SqlDataSource1" DataTextField="State" DataValueField="State" 
                        Font-Names="Verdana" Font-Size="Small" Height="25px" Width="147px">
                    </asp:DropDownList>
                    </a>
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
                    Font-Size="8pt" BorderColor="#CCFFFF" BorderStyle="Solid" 
                    DocumentMapCollapsed="True" Width="835px">
                    <LocalReport ReportPath="Report3.rdlc">
                        <DataSources>
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource2" Name="DataSet1" />
                            <rsweb:ReportDataSource DataSourceId="ObjectDataSource2" Name="DataSet2" />
                        </DataSources>
                    </LocalReport>
                </rsweb:ReportViewer>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                    SelectMethod="GetData" 
                    TypeName="sap.Web.DataSet1TableAdapters.locationTableAdapter" 
                    OldValuesParameterFormatString="original_{0}" DeleteMethod="Delete" 
                           InsertMethod="Insert" UpdateMethod="Update">
                    <DeleteParameters>
                        <asp:Parameter Name="Original_Locationid" Type="Decimal" />
                    </DeleteParameters>
                    <InsertParameters>
                        <asp:Parameter Name="Locationid" Type="Decimal" />
                        <asp:Parameter Name="Street" Type="String" />
                        <asp:Parameter Name="Village_town_city" Type="String" />
                        <asp:Parameter Name="District" Type="String" />
                        <asp:Parameter Name="State" Type="String" />
                    </InsertParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="Street" Type="String" />
                        <asp:Parameter Name="Village_town_city" Type="String" />
                        <asp:Parameter Name="District" Type="String" />
                        <asp:Parameter Name="State" Type="String" />
                        <asp:Parameter Name="Original_Locationid" Type="Decimal" />
                    </UpdateParameters>
                </asp:ObjectDataSource> 
                </div>                
                </div>
				<div id="ByDisease" runat="server"></div>
		
			<div>
				<h3><a href="#">ByDisease</a></h3>
				<div id="ByArea" runat="server">
                    <asp:DropDownList ID="DropDownList5" runat="server" 
                        DataSourceID="SqlDataSource3" DataTextField="Disease" DataValueField="Disease" 
                        Height="34px" Width="160px">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:AKAV1ConnectionString1 %>" 
                        SelectCommand="SELECT [Disease] FROM [disease]"></asp:SqlDataSource>
                    <br />
                    <rsweb:ReportViewer ID="ReportViewer2" runat="server" BorderColor="#CCFFFF" 
                        BorderStyle="Solid" Font-Names="Verdana" Font-Size="8pt" Width="843px">
                        <LocalReport ReportPath="Report3.rdlc">
                            <DataSources>
                                <rsweb:ReportDataSource DataSourceId="ObjectDataSource2" Name="DataSet1" />
                                <rsweb:ReportDataSource DataSourceId="ObjectDataSource2" Name="DataSet2" />
                            </DataSources>
                        </LocalReport>
                    </rsweb:ReportViewer>
                
			    </div>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:AKAV1ConnectionString1 %>" 
                        SelectCommand="SELECT * FROM [disease]">
                    </asp:SqlDataSource>
                    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" 
                        OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
                        TypeName="sap.Web.DataSet1TableAdapters.disease_incidenceTableAdapter">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="DropDownList5" DefaultValue="fever" 
                                Name="disease" PropertyName="SelectedValue" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                
			</div>		
			    <div>
				    <h3><a href="#">ByTimePeriod</a></h3>
				    <div id="ByTimePeriod" runat="server">
                    <asp:DropDownList ID="DropDownList6" runat="server" Height="18px" Width="120px">
                        <asp:ListItem Selected="True">1989</asp:ListItem>
                        <asp:ListItem>2000</asp:ListItem>
                        <asp:ListItem>2001</asp:ListItem>
                        <asp:ListItem>2002</asp:ListItem>
                        <asp:ListItem>2003</asp:ListItem>
                        <asp:ListItem>2004</asp:ListItem>
                        <asp:ListItem>2005</asp:ListItem>
                        <asp:ListItem>2006</asp:ListItem>
                        <asp:ListItem>2007</asp:ListItem>
                        <asp:ListItem>2008</asp:ListItem>
                        <asp:ListItem>2009</asp:ListItem>
                        <asp:ListItem>2010</asp:ListItem>
                        </asp:DropDownList>
                    <asp:DropDownList ID="DropDownList7" runat="server" Height="18px" Width="101px">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        </asp:DropDownList>
                    <asp:DropDownList ID="DropDownList8" runat="server" Height="18px" Width="99px">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                        </asp:DropDownList>
                        <rsweb:ReportViewer ID="ReportViewer3" runat="server" Font-Names="Verdana" 
                            Font-Size="8pt" Width="871px">
                            <LocalReport ReportPath="Report4.rdlc">
                                <DataSources>
                                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource3" Name="DataSet1" />
                                </DataSources>
                            </LocalReport>
                        </rsweb:ReportViewer>
                        <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" 
                            SelectMethod="GetData" 
                            TypeName="sap.Web.DataSet1TableAdapters.TimeTableAdapter" 
                            OldValuesParameterFormatString="original_{0}">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="DropDownList6" Name="year" 
                                    PropertyName="SelectedValue" Type="Int32" />
                                <asp:ControlParameter ControlID="DropDownList7" Name="quarter" 
                                    PropertyName="SelectedValue" Type="Int32" />
                                <asp:ControlParameter ControlID="DropDownList8" Name="month" 
                                    PropertyName="SelectedValue" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </div>
			    </div>
			</div>
			</div>		
		</div>         
   </div>
  
   
    </form>
        
</body>
</html>


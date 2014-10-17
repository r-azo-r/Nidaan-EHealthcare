<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="sap.Web.HomePage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta content="en-us" http-equiv="Content-Language" />
    <title></title>
   	    <script type="text/javascript" src="js/jquery-1.4.2.min.js"></script>
		<script type="text/javascript" src="js/jquery-ui-1.8rc2.custom.min.js"></script>
    
    
     <script type="text/javascript">
         function onSilverlightError(sender, args) {
             var appSource = "";
             if (sender != null && sender != 0) {
                 appSource = sender.getHost().Source;
             }

             var errorType = args.ErrorType;
             var iErrorCode = args.ErrorCode;

             if (errorType == "ImageError" || errorType == "MediaError") {
                 return;
             }

             var errMsg = "Unhandled Error in Silverlight Application " + appSource + "\n";

             errMsg += "Code: " + iErrorCode + "    \n";
             errMsg += "Category: " + errorType + "       \n";
             errMsg += "Message: " + args.ErrorMessage + "     \n";

             if (errorType == "ParserError") {
                 errMsg += "File: " + args.xamlFile + "     \n";
                 errMsg += "Line: " + args.lineNumber + "     \n";
                 errMsg += "Position: " + args.charPosition + "     \n";
             }
             else if (errorType == "RuntimeError") {
                 if (args.lineNumber != 0) {
                     errMsg += "Line: " + args.lineNumber + "     \n";
                     errMsg += "Position: " + args.charPosition + "     \n";
                 }
                 errMsg += "MethodName: " + args.methodName + "     \n";
             }

             throw new Error(errMsg);
         }
    </script>

    <!-- <link type="text/css" href="css/ui-lightness/jquery-ui-1.7.2.custom.css" rel="stylesheet" />	-->
   <link type="text/css" href="css/custom-theme/jquery-ui-1.8rc2.custom.css" rel="stylesheet" />
    <style type="text/css">
        #Top
        {
            width:100%;
            height:120px;
            background:#070842;
            background-repeat:no-repeat;
            float: left;
            font-family:"Copperplate Gothic Bold";
            font-size:50px;
            text-align:center;
            vertical-align:middle;
            color:Maroon;
            border:thin;
        	border-style:double;
        }
        #LeftPanel
        {
            width: 26%;
            height:556px;
            float: left;
            
        	
        }
        #CentrePanel
        {
            width: 49%;
       		
            float:left;
            height: 51px;
        }
         #RightPanel
        {
            width: 31%;
            float: left;
            height: 261px;
        }
        
         #silverlightControlHost {
	    height: 100%;
	    text-align:center;
    }


       .table1{width:100%;}
      .tableheading1{background-color: Maroon;color:White;font-family:Copperplate Gothic Bold;font-size:20px;}
      .tableheading2{background-color:  #eeeeee ;color:Black;font-family:Copperplate Gothic Bold;font-size:15px;text-align:center;}
      .tablerow1{background-color:  #eeeeee;font-family:" Arial Black";font-size:15px;text-align:center;}
      .tab{width:16%}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="Top">
      <img src="Images/Logo_Final.png"></img>
       </div>
    <div id="LeftPanel">
    <h4 style="font-family:"Copperplate Gothic Bold;text-align:center;vertical-align:middle;color:Maroon;"></h4>
 
     <br />   
    <p>
    <div id="silverlightControlHost">
        <object data="data:application/x-silverlight-2," 
              type="application/x-silverlight-2" 
              style="position:absolute;top:140px; left:12px;height:370px; width:25%;" >
		  <param name="source" value="ClientBin/calc.xap"/>
		  <param name="onError" value="onSilverlightError" />
		  <param name="background" value="white" />
		  <param name="minRuntimeVersion" value="3.0.40818.0" />
		  <param name="autoUpgrade" value="true" />
		  <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=3.0.40818.0" style="text-decoration:none">
 			  <img src="http://go.microsoft.com/fwlink/?LinkId=108181" alt="Get Microsoft Silverlight" style="border-style:none"/>
		  </a>
	    </object><iframe id="_sl_historyFrame" style="visibility:hidden;height:0px;width:0px;border:0px"></iframe></div>
        </p>
    </div>
    <div id="CentrePanel">
     <h2>&nbsp;</h2>
     <p>&nbsp;</p>
    </div>
    <div id="RightPanel" style="margin-left:15%">
         <p style="top: 477px; left: -222px; position: absolute; height: 81px; width: 646px; font-family:@Arial Unicode MS;font-size:larger; text-align:justify;margin-left: 590px;color:#070842">
             <b>NIDAAN</b>-a health care solution is unique in terms of its basic medical 
             analysis tool and storage of medical records. We eliminate the tedious paper&nbsp; 
             work and also provide with features like checking previous medical data for 
             patients,obtain timely software based health advice ,connecting&nbsp; you with 
             doctors and chemists.In all ,at <b>NIDAAN</b> <i>We Care..</i>.</p>
     <fieldset style="top: 193px; left: 490px; position: absolute; height: 277px; width: 325px">
     <legend>Sign In</legend>
     <br />
     <table style="width: 100%">
    
		 <tr>
			 <td style="width: 54px">Login ID</td>
			 <td><input name="txtlogin" type="text" style="width: 100%; height: 30px;" />
</td>
		 </tr>
		 <tr>
			 <td style="width: 54px">Password</td>
			 <td> <input name="txtpassword" type="password" style="width: 100%; height: 30px;" /></td>
		 </tr>
		 <tr>
		 <td style="width: 54px">Type</td>
		 </tr>
	 </table>
         <table style="width: 100%">
		     <tr>
		 <td><input type="radio" name="rdotype" checked  value="1"/>
    	 <asp:ImageButton id="ImageButton2" runat="server" Height="32px" 
                 ImageUrl="~/Images/doctor-symbol.jpg" Width="32px" AlternateText="Doctor" />
                 <input type="radio" name="rdotype"  value="3"/>
		 &nbsp;<asp:ImageButton id="ImageButton3" runat="server" Height="42px" 
                 ImageUrl="~/Images/12117617901674834939ivak_kiosk-terminal_svg_hi.png" 
                 Width="25px" AlternateText="Entreprenuer" />
               <input type="radio" name="rdotype" value="2"/>
		 &nbsp;&nbsp;<asp:ImageButton ID="ImageButton4" runat="server" Height="32px" 
                 ImageUrl="~/Images/canstock0278224.jpg" Width="32px" />
                 <input type="radio" name="rdotype" value="4"/>
             <asp:ImageButton ID="ImageButton5" runat="server" Height="32px" 
                 ImageUrl="~/Images/logo_redcross0206.jpg" Width="32px" />
             </td>
		 </tr>
		 <tr>
		 <td align="center">
            
		     <asp:Button ID="btnLogin" runat="server" Text="Login" Height="30px"  OnClick="btnLogin_Click"/>
		<a href="#">&nbsp;Register</a><br /> </asp:Label></td>
        
		 </tr>
          <tr><td colspan="2"><asp:Label ID="lblerror" runat="server" Text="" ForeColor="Red"/></td></tr>
	 </table>
     </fieldset>
 
    </div>
    <br />
     <div >
         </div>
    </form>
</body>
</html>

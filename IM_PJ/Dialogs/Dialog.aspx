<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialog.aspx.cs" Inherits="WebUI.Admin.Dialogs.Dialog" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        html, body {
            margin: 0;
            padding: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
        <telerik:RadFileExplorer RenderMode="Lightweight" runat="server" ID="FileExplorer1" Width="780px" Height="600px"
            OnClientFileOpen="OnClientFileOpen" Skin="MetroTouch">
            <Configuration ViewPaths="~/uploads" />
        </telerik:RadFileExplorer>
        <script>
            (function (global, undefined) {
                //A function that will return a reference to the parent radWindow in case the page is loaded in a RadWindow object
                function getRadWindow() {
                    var oWindow = null;
                    if (window.radWindow) oWindow = window.radWindow;
                    else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                    return oWindow;
                }

                function OnClientFileOpen(sender, args) {// Called when a file is open.
                    var item = args.get_item();

                    //If file (and not a folder) is selected - call the OnFileSelected method on the parent page
                    if (item.get_type() == Telerik.Web.UI.FileExplorerItemType.File) {
                        // Cancel the default dialog;
                        args.set_cancel(true);

                        // get reference to the RadWindow
                        var wnd = getRadWindow();

                        //Get a reference to the opener parent page using RadWndow
                        var openerPage = wnd.BrowserWindow;

                        //if you need the URL for the item, use get_url() instead of get_path()
                        openerPage.OnFileSelected(item.get_path());// Call the method declared on the parent page

                        //Close the window which hosts this page
                        wnd.close();
                    }
                }

                global.OnClientFileOpen = OnClientFileOpen;
            })(window);
        </script>
    </form>
</body>
</html>

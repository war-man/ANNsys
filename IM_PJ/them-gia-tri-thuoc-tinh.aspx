<%@ Page Title="Thêm giá trị thuộc tính" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="them-gia-tri-thuoc-tinh.aspx.cs" Inherits="IM_PJ.them_gia_tri_thuoc_tinh" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main-wrap">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panelborderheading">
                        <div class="panel-heading clear">
                            <h3 class="page-title left not-margin-bot">Thêm giá trị thuộc tính</h3>
                        </div>
                        <div class="panel-body">
                            <asp:Panel ID="Parent" runat="server">
                            <div class="form-row">
                                <asp:Label ID="lblError" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Tên thuộc tính 
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="ddlVariable"
                                       Display="Dynamic" ErrorMessage="(*)" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <div class="row-right">
                                    <asp:DropDownList runat="server" ID="ddlVariable" CssClass="form-control" DataTextField="VariableName"
                                        DataValueField="ID" AppendDataBoundItems="True" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlVariable_SelectedIndexChanged" />
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Giá trị
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="ddlVariable"
                                        Display="Dynamic" ErrorMessage="(*)" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                                </div>
                                <div class="row-right">
                                    <asp:DropDownList runat="server" ID="ddlVariableValue" CssClass="form-control" DataTextField="VariableValue"
                                        DataValueField="ID" AppendDataBoundItems="True"/>
                                </div>
                            </div>
                            <div class="form-row">
                                <div class="row-left">
                                    Ẩn                   
                                </div>
                                <div class="row-right">
                                    <asp:CheckBox ID="chkIsHidden" runat="server" />
                                    <div class="clear">                                        
                                        <asp:Button ID="btnLogin" runat="server" CssClass="btn primary-btn fw-btn not-fullwidth" Text="Tạo mới" OnClick="btnLogin_Click" />
                                        <asp:Literal ID="ltrBack" runat="server"></asp:Literal>                                        
                                    </div>
                                </div>
                            </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </div>        
    </main>
    <telerik:RadAjaxManager ID="rAjax" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnSave">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ddlVariable">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Parent" LoadingPanelID="rxLoading"></telerik:AjaxUpdatedControl>
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
</asp:Content>

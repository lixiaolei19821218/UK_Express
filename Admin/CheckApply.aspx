<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckApply.aspx.cs" Inherits="Admin_CheckApply" MasterPageFile="~/Admin/Admin.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <title>充值审核</title>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="Server">
    <div class="uitopb uitopb-border mt10">
        <div class="table-div">
            <table class="table-list">
                <tr>
                    <th>账号</th>
                    <th>方式</th>
                    <th>金额(£)</th>
                    <th>凭证</th>
                    <th>时间</th>
                    <th>处理</th>
                </tr>
                <asp:Repeater runat="server" SelectMethod="GetRechargeApplys" ItemType="RechargeApply">
                    <ItemTemplate>
                        <tr>
                            <td><%#Item.User %></td>
                            <td><%#Item.RechargeChannel.Name %></td>                            
                            <td><%#Item.ApprovedAmount.HasValue? Item.ApprovedAmount.Value.ToString() : "－" %></td>
                            <td>
                                <a href="<%#Item.Evidence %>" target="_blank" title="付款凭证"><%#string.IsNullOrEmpty(Item.Evidence) ? string.Empty: "付款凭证" %></a><br />
                            </td>
                            <td><%#Item.Time %></td>
                            <td><input type="submit" value="通过" class="btn btn-1" /><input type="submit" value="不通过" class="btn btn-1"/></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
</asp:Content>

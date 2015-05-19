<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RechargeList.aspx.cs" Inherits="accounts_UserCentre_RechargeList" MasterPageFile="~/accounts/UserCentre/UserCentre.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="Server">
    <title>充值记录</title>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="Server">
    <h2>充值明细</h2>
    <div class="sub-nav">
        <span class="rt">充值<em>£0</em>，剩余<em>£0</em>(赠送<em>£0</em>)</span>
        <a href="/index.php?c=bank&a=user_list" class="back">返回</a>
    </div>
    <div class="uitopb uitopb-border mt10">
        <div class="table-div">
            <table class="table-list">
                <tr>
                    <th>方式</th>
                    <th>金额(£)</th>
                    <th>凭证</th>
                    <th>时间</th>
                    <th>处理</th>
                </tr>
                <asp:Repeater runat="server" SelectMethod="GetRechargeApplys" ItemType="RechargeApply">
                    <ItemTemplate>
                        <tr>
                            <td><%#Item.RechargeChannel.Name %></td>                            
                            <td><%#(Item.ApprovedAmount.HasValue? Item.ApprovedAmount.Value : 0.0m) %></td>
                            <td>
                                <a href="<%#Item.Evidence %>" target="_blank" title="付款凭证">付款凭证</a><br />
                            </td>
                            <td><%#Item.Time %></td>
                            <td><%#Item.IsApproved %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </div>
    <div class="pager"><font style="color: red;">1</font>&nbsp;</div>
</asp:Content>

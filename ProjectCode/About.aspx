<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="ProjectCode.About" %>

<%@ Register assembly="EventCalendarControl" namespace="EventCalendar" tagprefix="cc1" %>
<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>
        <asp:TextBox ID="TestBox" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
        <asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem>Month</asp:ListItem>
            <asp:ListItem>Week</asp:ListItem>
        </asp:DropDownList>
    </h2>
    <h3>Your application description page.<asp:Button ID="Button2" runat="server" Text="Button" />
    </h3>
    <p>Use this area to provide additional information.<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ts348ConnectionString %>" SelectCommand="SELECT * FROM [Project_Table]"></asp:SqlDataSource>
        
        <asp:GridView ID="TimeGrid" runat="server">

        </asp:GridView>
        
        <asp:TextBox ID="txtProjectID" runat="server"></asp:TextBox>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="404px">
            <Columns>
                <asp:BoundField DataField="TaskDes" HeaderText="TaskDes" SortExpression="TaskDes" />
                <asp:BoundField DataField="PLevel" HeaderText="PLevel" SortExpression="PLevel" />
                <asp:BoundField DataField="StartTime" HeaderText="StartTime" SortExpression="StartTime" />
                <asp:BoundField DataField="ENDTime" HeaderText="ENDTime" SortExpression="ENDTime" />
                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
                <asp:BoundField DataField="Task" HeaderText="Task" SortExpression="Task" />
            </Columns>
        </asp:GridView>
        
        &nbsp;
        
        <asp:Chart ID="Chart1" runat="server" AlternateText="Chart" Width="1047px">
            <Series>
                <asp:Series ChartType="RangeBar" Name="Series1" YValuesPerPoint="2" YValueType="DateTime">
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
        <asp:Chart ID="TestChart" runat="server" Width="464px" OnDataBound="ChartExample_DataBound">
           
            <Series>
                <asp:Series ChartType="RangeBar" Name="Series1" YValuesPerPoint="2">
                    <Points>
            <asp:DataPoint AxisLabel="Sunday" YValues="3,6" />   
            <asp:DataPoint AxisLabel="Saturday" YValues="3,4" />
            <asp:DataPoint AxisLabel="Friday" YValues="3,5" />
            <asp:DataPoint AxisLabel="Thursday" YValues="4,7" />
            <asp:DataPoint AxisLabel="Wednestday" YValues="6,5" />
            <asp:DataPoint AxisLabel="Tuesday" YValues="15,2" />
            <asp:DataPoint AxisLabel="Monday" YValues="10,4" />

         </Points>
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1">
                    <AxisY>
                        <ScaleView SizeType="Days" />
                    </AxisY>
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    </p>
    </asp:Content>

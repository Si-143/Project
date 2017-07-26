<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ProjectCode._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">



    <div class="jumbotron">
  
        <h1 ><span style="font-size: large">Login

            </span><asp:TextBox CssClass="divider" ID="UserName" runat="server" Height="20px" Width="100px"></asp:TextBox>        
            <asp:TextBox  ID="PasswordBox" runat="server"></asp:TextBox>
        </h1>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ts348ConnectionString %>" SelectCommand="SELECT * FROM [Project_Table]"></asp:SqlDataSource>
       <h2>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Reload" Height="32px" Width="146px" />
            
        <asp:DropDownList ID="DropDownList1" runat="server" Height="26px" Width="93px">
            <asp:ListItem>Month</asp:ListItem>
            <asp:ListItem>Week</asp:ListItem>
        </asp:DropDownList>
           </h2>
         <p>
         <asp:Chart ID="Chart1" runat="server" AlternateText="Chart" Width="909px" Height="276px">
            <Series>
                <asp:Series ChartType="RangeBar" Name="Series1" YValuesPerPoint="2" YValueType="DateTime" ChartArea="ChartArea1">
                </asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1">
                </asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
        </p>
        
        <asp:Button ID="arrange" runat="server" OnClick="arrange_Click" Text="Rearrange" />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="75px" Height="16px">
            <Columns>
                <asp:BoundField DataField="TaskDes" HeaderText="TaskDes" SortExpression="TaskDes" />
                <asp:BoundField DataField="PLevel" HeaderText="PLevel" SortExpression="PLevel" />
                <asp:BoundField DataField="StartTime" HeaderText="StartTime" SortExpression="StartTime" />
                <asp:BoundField DataField="ENDTime" HeaderText="ENDTime" SortExpression="ENDTime" />
                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
                <asp:BoundField DataField="Task" HeaderText="Task" SortExpression="Task" />
                <asp:BoundField DataField="Path" HeaderText="Path" SortExpression="Path" />
            </Columns>
        </asp:GridView>
  

    <div class="row">
        <div class="col-md-4">
           
            
      
        <p> GridView<asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="CPM" />
              <asp:GridView ID="TimeGrid" runat="server" Height="24px" Width="131px">

        </asp:GridView>
            </p>
        </div>
 
  
    </div> </div>
</asp:Content>

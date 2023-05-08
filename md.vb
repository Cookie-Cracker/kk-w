Imports System.Net.Mail
Imports System.IO
Imports System.Text
Imports System.IO.Path
Module md

#Region "Public Variables"
    ' ------- Change #: 06/23/2022 - 1
    ' ------ FTP info -------------------------------------
    ' 40.113.246.191
    ' username: octopi
    ' passw:qm9RVm2

    Public Version As String = "1.0.0.0.0"
    Public ws As New MSI_Solution.net.azurewebsites.kingocean.MSL_WS.MSL_WS
    'MSI_Solution.net.azurewebsites.kingocean.MSL_WS   ' CCWindowsCL.localhost.CCServ()

    Public date_Format As String = ""
    Public Time_Short_Format As String = ""
    Public Time_Format As String = ""
    Public Number_Format_Dig_Separator As String = ""
    Public Number_Format_Thousand_Separator As String = ""
    Public msg = "Do you want to continue?"
    Public msg_title = "Warning"
    Public msg_style = MsgBoxStyle.YesNo

    Public strSQL_Query As String = ""

    Public OceanACE_Account_Code As String = "KOSL"
    Public OceanACE_partyName As String = "King Ocean Services"
    Public OceanACE_partyStreet As String = "13155 NW 19th Lane"
    Public OceanACE_city As String = "Sweetwater"
    Public OceanACE_postalCode As String = "33182"
    Public OceanACE_state As String = "FL"
    Public OceanACE_countryCode As String = "US"
    Public OceanACE_phone As String = "+1 (305) 597-1369"

    Public OceanACE_Contact_Name As String = "Kim Highsmith"
    Public OceanACE_Contact_phone As String = "(305) 970-8509"
    Public wAutoScrollPos As System.Drawing.Point
    Public UserName As String = ""
    Public UserCode As Integer = 0
#End Region

#Region "Connections"
    Public SQL_Enviromme As String = "MSI"   '"GDZ"
    Public DB_Server As String = "kingocean.database.windows.net"
    Public DB_DataBase As String = "kingocean"
    '"atb0n2zc"

    Public strConnection_Local As String = "Data Source=MSL;Initial Catalog=HydeSolution;User ID=KO;Password=!atb0n2zc@"
    '"Data Source=MSL;Initial Catalog=HydeSolution;User ID=KO;Password=atb0n2zc*;Connection Timeout=600"
    Public strConnection_Sys As String = "Data Source=kingocean.database.windows.net;Initial Catalog=SCSolution;User ID=kingadmin;Password=!King@cean*;Connection Timeout=600"
    Public Const strConnect_KingOcean As String = "Data Source=kingocean.database.windows.net;Initial Catalog=SCSolution;User ID=kingadmin;Password=!King@cean*;Connection Timeout=600"
    Public Const strConnect_KOGA As String = "Data Source=kingocean.database.windows.net;Initial Catalog=KOGA;User ID=kingadmin;Password=!King@cean*;Connection Timeout=600"

    Public Const AppToken As String = "AgAAAA**AQAAAA**aAAAAA**SM3ZTA**nY+sHZ2PrBmdj6wVnY+sEZ2PrA2dj6AEk4OjDJWCowSdj6x9nY+seQ**MB8AAA**AAMAAA**dkDz+ui36iq7KRdrvLsxCILVkM/t4nSm1RpG0rGEtZFWJoVfeqgHvHjdhB72EOIU5+8gX5K8SQSovRwV8vMB9vejso/zCOPdLiOFSGM/ZhB2gzPud+5xhaKZhVY4hKWznakY8FREh1Ml7v9c9zYCPz3QUa9XwiqOsFTBzEqplJX0dyW0tj4nKLjfxRltNNrJmsydbF7KAjJ0Jfv4IsXXKu1kR81MWbv9GoHfqy1oa1fzky3EiXec4W5JiHCGxbgujT4lJUjOghd60L0FANoc+slgn/+4wcsbGrL/ZMeEfPxu4cwwStaDpBtgK1a7KV+IQj7+m4XV97v5c/4Yd/Wp9u4ApXUS4pmU9EZVYEZOhXdYsTyG2XiGp6a2XNq6esrFW9+VsAnui6qyP33ZiE+dMBQcRKoO1o3yptoP5AlQOw56zl9eVqb6A/mn6BD7LNKPDHph1no+epFe/KD5jCo8q7mITiXeLiYEen0BJonLeVbi52uMth/szLI8vh0oUTgwOrRnh18fzA96uO6x3DrRUNlqMzlvG1Y9/XaWgAIcXDivtBd7dZqpqBpHvIYu5mAbbza6ATiQa0oXifWAWNQk7p2jeRkonD4e3jXs7kXlz0BPV1+5voh53IhpvLZ/pEgeKDelVOshFPn7m/HxpvisrBt7bU9gd1q+FTULHEUV2WsaUHhMwEbqkCGGQHR4yIL0jw4o5/dNCLPkelBJhQKLQJXngZ9HrqHdPSqhg/icch0A+bC4CAg9GAdAY5lLZrKI"
    Public strConnect As String = "Data Source=kingocean.database.windows.net;Initial Catalog=SCSolution;User ID=kingadmin;Password=!King@cean*;Connection Timeout=600"


    Public GDZConect As String = "Provider=MSDASQL.1;Persist Security Info=False;Data Source=gdz-isis;Extended Properties="
    'DSN=gdz-isis;DATAPATH=\\AMMA01\Apps\gdz-isis;DDFPATH=\\AMMA01\Apps\gdz-isis;NullEnabled=no;FeaturesUsed=yes;AccessFriendly=no;DateFormat=mdy;"

    Public GDZConnect As String = "Driver={Pervasive ODBC Interface};DBQ=GDZISIS;"
    '"Driver={Pervasive ODBC Client Interface}; ServerName=cBajo-PC;ServerDSN=GDZ-ISIS;PvTranslate=auto;"
    '
    '"BOB=;DSN=GDZ-ISIS;LoginScript=;AccessFriendly=no;DDFPATH=\\c:gdz;DATAPATH=\\c:gdz;DateFormat=mdy;TranslateDLL=;TranslateOption=;FeaturesUsed=yes;NullEnabled=no"
#End Region

#Region "Line Company"
    Public Line_default_name As String = "KING OCEAN SERVICES LTD."
    Public Line_default_number As Integer = 1
    Public GL_Company As Integer = 1
    Public GL_Company_Name As String = "KING OCEAN SERVICES LTD."
    Public GL_Company_Address As String = "13155 NW 19th Lane" & Chr(13) & Chr(10) & "Sweetwater, FL, 33182 USA" & Chr(13) & Chr(10) & "(305) 591.7595"
    Public CmpLin1 As String = "KING OCEAN SERVICES LTD."
    Public CmpLin2 As String = "13155 NW 19th Lane, Sweetwater, FL, 33182, (305) 591.7595"
    Public Port_default As String = "PORT EVERGLADES"
    Public PortL As Integer = 100
    Public PortD As Integer = 200
    Public AR_Acc As Integer = 1205

    Public AP_Acc As Integer = 3021
    Public Ocean_Freight_Acc As Integer = 4120
    Public Bank_Acc As String = "1026"
    Public Default_Trucker As String = "1026"
    Public Default_WUnit As String = "KG"
    Public Default_MUnit As String = "M3"
    Public eResp As String = ""
    Public v_L1 As String = ""
    Public strSQL As String = ""
    Public Default_Terminal_2 As Integer = 105
    Public Default_Terminal_1 As Integer = 1151
    Public KOGA_Booking_Address As String = "quotes@kogaship.com" & vbCrLf & "Ofc +1-713-589-2141 / Cel +1-832-540-9022" & vbCrLf & "777 S Post Oak Ln - Suite 1751 - Houston, TX 77056" & vbCrLf & "On behalf of King Ocean Shipping"
#End Region

#Region "Default_Info"
    Public vTest As String = ""
    Public Yard_D As Integer = 1151    ' ------- Yard account
    Public Warehouse_D As String = "1026"  ' ------- Warehouse account
    Public Warehouse_Loc As Integer = 0     ' ------- Warehouse Loc
    Public DR_Current As Integer = 0
    Public Flag_Modo_DR As Integer = 0
    Public Warehouse_Plase_origin As Integer = 7011 '30048 ' ------ Wahrehouse, Place Origin
    Public Warehouse_Shipper As Integer = 25986
    Public Trucker_D As String = "1026"     ' ------- Trucker account
    Public Port_Loading_For_DR As Integer = 100     ' ------- Port of Loading for DRs
    Public Agent_D As Integer = 3014
    Public Country_D As String = "USA"
    Public psw_acc = "zxc765q!"
    Public Flag_Admin As String = "N"
    Public OceanFreight_Charge As Integer = 1
    Public DR_Process_Type As Integer = 0
#End Region

#Region "Validations"
    Public Function Validations_Int(ByVal Field_To_Check As String) As Boolean
        If Not IsNumeric(Field_To_Check) Then
            MsgBox("This field must be numeric,..")
            Return False
        Else
            Return True
        End If
    End Function
    Public Function CheckApostrophe(ByVal StrToCheck As String, ByVal StrChecked As String)
        Dim wLong As Integer
        Dim MyPos As Integer
        Dim wDifLong
        MyPos = InStr(Trim(StrToCheck), "'")
        If MyPos = 0 Then
            StrChecked = StrToCheck
        Else
            wLong = Len(Trim(StrToCheck))
            wDifLong = (wLong - MyPos) + 1
            StrChecked = Mid(StrToCheck, 1, MyPos - 1) & "'" & Mid(StrToCheck, MyPos, wDifLong)
        End If
        Return (StrChecked)
    End Function
    Public Function Trucker_Insure_validation(ByVal nTrucker As Integer) As Boolean
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "Select Format(isnull(Trucker_Ins_Date,''),'yyyy-MM-dd') as Trucker_Ins_Date From CM_System Where Company_Number = " & nTrucker, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            If Len(Trim(ds.Tables(0).Rows(0).Item("Trucker_Ins_Date"))) > 0 Then
                If ds.Tables(0).Rows(0).Item("Trucker_Ins_Date") > Format(System.DateTime.Today, "yyyy-MM-dd") Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Else
            Return False
        End If
        ds = Nothing
    End Function
#End Region

#Region "Code of Move" ' ------- RCVE, RCVF, LODE, etc,.....
    Public Function Eq_Movement_Status(ByVal Code As String) As DataSet
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT DISTINCT CURRENT_STATUS FROM  Eq_Move_Link_Rules where CURRENT_STATUS like '" & Trim(Code) & "%' order by current_status", 1)
        Return ds
    End Function

    Public Function Ask_Equipment_is_Container(ByVal Equipment_Type As String) As Boolean
        Dim ds As New DataSet
        strSQL = "Select Equipment_Class From EqpMts Where Equipment_Type = '" & Trim(Equipment_Type) & "' and Isnull(Equipment_Class,'') = 'A'"
        ds = ws.GetDataset(strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region

#Region "DR"
    Public Function DR_Last_DRRepack(ByVal Warehouse As Integer) As Integer
        Dim nDr_L As Integer = 0
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT Top 1 DR_NUMBER FROM  DockReceipts Where DR_Stevedoring = " & Warehouse & " and DR_Number > 32000000 and DR_Number < 32900000 Order by Dr_number desc", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            nDr_L = ds.Tables(0).Rows(0).Item("Dr_number")
            Return nDr_L
            nDr_L = Nothing
            ds = Nothing
        Else
            Return 32000000
        End If
    End Function
    Public Function Last_DR(ByVal DR As Integer) As Integer
        Dim nDr_L As Integer = 0
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT DR_NUMBER FROM  DockReceipts Where DR_Stevedoring = " & DR & " Order by Dr_number desc", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            nDr_L = ds.Tables(0).Rows(0).Item("Dr_number")
            Return nDr_L
            nDr_L = Nothing
            ds = Nothing
        End If
    End Function
    Public Function DR_Audit_BY_DR(ByVal DR As Integer) As DataSet
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT DR, Description, Created_ON, Created_By, Old_value, isnull(Username,'') as UserName FROM DR_Journal
                         WHERE  (DR =" & DR & ") Order By uid desc", 1)
        Return ds
    End Function
#End Region

#Region "GDZ"
#End Region

#Region "Ports"
    Public Function Ports_name(ByVal name As String) As DataSet
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT Top 7 port_name,Port_number FROM  Ports where port_Name like '" & Trim(name) & "%' order by port_name", 1)
        Return ds
    End Function

    Public Function Ports_get_Customs_Office_Code(ByVal Port As Integer) As String
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT isnull(Asycuda_Customs_Office_Code,'') as Code FROM Ports where port_Number =" & Port, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            If Len(Trim(ds.Tables(0).Rows(0).Item("Code"))) = 0 Then
                ds.Clear()
                ds = ws.GetDataset(md.strConnect, "SELECT isnull(Port_Int_Code,'') as Code FROM Ports where port_Number =" & Port, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    Return Trim(ds.Tables(0).Rows(0).Item("Code"))
                End If
            Else
                Return Trim(ds.Tables(0).Rows(0).Item("Code"))
            End If
        End If
    End Function

    Public Function Ports_get_Eq_Type_Code(ByVal Port As Integer) As String
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT isnull(Asycuda_Eq_Type_Code,'') as Code FROM Ports where port_Number =" & Port, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            If Len(Trim(ds.Tables(0).Rows(0).Item("Code"))) = 0 Then
                Return ""
            Else
                Return Trim(ds.Tables(0).Rows(0).Item("Code"))
            End If
        End If
    End Function

    Public Function Ports_get_Port_Brother(ByVal Port As Integer) As Integer
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT isnull(Port_Brother,0) as Port_Brother FROM Ports where port_Number =" & Port, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            If Port = ds.Tables(0).Rows(0).Item("Port_Brother") Then
                Return 0
            Else
                Return ds.Tables(0).Rows(0).Item("Port_Brother")
            End If
        Else
            Return 0
        End If
    End Function

    Public Function Ports_get_Carrier_Code(ByVal Port As Integer) As String
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT isnull(Asycuda_Carrier_Code,'') as Code FROM Ports where port_Number =" & Port, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            If Len(Trim(ds.Tables(0).Rows(0).Item("Code"))) = 0 Then
                Return ""
            Else
                Return Trim(ds.Tables(0).Rows(0).Item("Code"))
            End If
        End If
    End Function

    Public Function Ports_get_Place_of_destination_Code(ByVal Port As Integer) As String
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT isnull(Asycuda_Place_destination_code,'') as Code FROM Ports where port_Number =" & Port, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            If Len(Trim(ds.Tables(0).Rows(0).Item("Code"))) = 0 Then
                Return ""
            Else
                Return Trim(ds.Tables(0).Rows(0).Item("Code"))
            End If
        End If
    End Function

    Public Function Ports_get_Weight_Unit(ByVal Port As Integer) As String
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT isnull(WUnit,'') as WUnit FROM Ports where port_Number =" & Port, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            If Len(Trim(ds.Tables(0).Rows(0).Item("WUnit"))) = 0 Then
                Return ""
            Else
                Return Trim(ds.Tables(0).Rows(0).Item("WUnit"))
            End If
        End If
    End Function

    Public Function Ports_get_Measure_Unit(ByVal Port As Integer) As String
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT isnull(MUnit,'') as MUnit FROM Ports where port_Number =" & Port, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            If Len(Trim(ds.Tables(0).Rows(0).Item("MUnit"))) = 0 Then
                Return ""
            Else
                Return Trim(ds.Tables(0).Rows(0).Item("MUnit"))
            End If
        End If
    End Function

    Public Function Ports_name_Jobs(ByVal name As String, ByVal Sailing As String) As DataSet
        Dim ds As New DataSet
        If Len(Trim(Sailing)) = 0 Then
            ds = ws.GetDataset(md.strConnect, "SELECT Top 7 port_name,Port_number FROM  Ports where port_Name like '" & Trim(name) & "%' order by port_name", 1)
        Else
            ds = ws.GetDataset(md.strConnect, "SELECT Top 7 j.Sailing_nro,p.PORT_NUMBER,p.PORT_NAME FROM Sailing_Master as j Inner Join 
                                                      Ports as p on j.Port = p.Port_number  where port_Name like '" & Trim(name) & "%' and
                                                                j.Sailing_nro = '" & Trim(Sailing) & "' order by p.Port_Name", 1)
        End If
        Return ds
    End Function

    Public Function Ports_code(ByVal code As Integer) As String
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT  port_name,Port_number FROM  Ports where port_code = " & Trim(code), 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Port_Name")
        Else
            Return ""
        End If
    End Function

    Public Function Ports_Name_x_Number(ByVal Number As Integer) As String
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT port_Name  FROM  Ports where Port_Number = " & Trim(Number), 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return Trim(ds.Tables(0).Rows(0).Item("Port_Name"))
        Else
            Return ""
        End If
    End Function

    Public Function Ports_Num_x_Name(ByVal Name As String) As Integer
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT Port_Number FROM Ports where Port_Name = '" & Trim(Name) & "'", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return Trim(ds.Tables(0).Rows(0).Item("Port_Number"))
        Else
            Return 0
        End If
    End Function

    Public Function Ports_Short_x_Number(ByVal Number As Integer) As String
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT Port_Short FROM  Ports where Port_Number = " & Trim(Number), 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Port_Short")
        Else
            Return ""
        End If
    End Function

    Public Function Ports_Number_x_Short_(ByVal Port_Short As String) As Integer
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT Port_Number FROM  Ports where Port_Short = '" & Trim(Port_Short) & "'", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Port_Number")
        Else
            Return 1
        End If
    End Function

    Public Function Ports_Country(ByVal Number As Integer) As String
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT isnull(Country,'') as Country FROM  Ports where Port_Number = " & Trim(Number), 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Country")
        Else
            Return ""
        End If
    End Function

    Public Function Ports_Int_Code_x_Number(ByVal Number As Integer) As String
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT Port_Int_Code FROM  Ports where Port_Number = " & Trim(Number), 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Port_Int_Code")
        Else
            Return ""
        End If
    End Function

    Public Function Ports_Name_x_ShortName(ByVal ShortName As String) As String
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT Port_Name FROM  Ports where Port_Short = '" & Trim(ShortName) & "'", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Port_Name")
        Else
            Return ""
        End If
    End Function

    Public Function Ports_Name_x_Port_Int_Code(ByVal Port_Int_Code As String) As String
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT Port_Name FROM  Ports where Port_Int_Code = '" & Trim(Port_Int_Code) & "'", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Port_Name")
        Else
            Return ""
        End If
    End Function

    Public Function Ports_number(ByVal code As Integer) As DataSet
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT  * FROM  Ports where port_Number = " & Trim(code), 1)
        Return ds
    End Function

    Public Function Ports_Master() As DataSet
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT port_name,Port_number,isNull(Port_Int_Code,'') as Port_Int_Code,Port_Short, Country_Code  FROM  Ports WHERE (ISNULL(PortPlace, 'N') = 'Y') Order by port_name", 1)
        Return ds
    End Function

    Public Function PLace_Master() As DataSet
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT port_name,Port_number,isNull(Port_Int_Code,'') as Port_Int_Code,Port_Short, Country_Code  FROM  Ports WHERE (ISNULL(Place, 'N') = 'Y') Order by port_name", 1)
        Return ds
    End Function

    Public Function Ports_Yard(ByVal code As Integer) As Integer
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT Yard FROM  Port_Yards where port_Number = " & Trim(code), 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Yard")
        Else
            Return 0
        End If
    End Function

    Public Function Ports_Name_Yard(ByVal Name As String) As Integer
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT Yard FROM Port_Yards where port_Number = " & Trim(md.Ports_Num_x_Name(Name)), 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Yard")
        Else
            Return 0
        End If
    End Function
#End Region

#Region "Sailing_Master"
    Public Function Job_BY_Job(ByVal Job As String) As DataSet
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT j.Sailing_nro, j.LINE_NAME, j.VESSEL_NAME, j.Voyage_nro, j.EST_DEPARTURE, j.PORT, p.PORT_NAME FROM Sailing_Master AS j INNER JOIN Ports AS p ON j.PORT = p.PORT_NUMBER WHERE (NOT (j.EST_DEPARTURE IS NULL)) AND (j.Sailing_nro = '" & Trim(Job) & "') ORDER BY j.EST_DEPARTURE, j.PORT", 1)
        Return ds
    End Function
    Public Function Job_BY_Voyage(ByVal Voyage As String) As DataSet
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT j.LINE_NAME, j.Sailing_nro, j.EST_DEPARTURE, j.VESSEL_NAME, j.Voyage_nro, j.PORT, p.PORT_NAME FROM Sailing_Master AS j INNER JOIN Ports AS p ON j.PORT = p.PORT_NUMBER WHERE (j.Voyage_nro = '" & Trim(Voyage) & "') AND (SUBSTRING(j.Sailing_nro, 10, 2) <> '00') AND (j.PORT <> 0) AND (YEAR(j.EST_DEPARTURE) = " & Year(System.DateTime.Today) & ") ORDER BY j.EST_DEPARTURE", 1)
        Return ds
    End Function
    Public Function Sailing_Audit_BY_Sailing(ByVal Job As String) As DataSet
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT Format(Created_Date,'MM/dd/yyyy') as Created_Date, Created_TIME, CREATED_BY, DESCRIPTION, Sailing_nro, Old_Value  FROM SailingAudit
                         WHERE  (Sailing_nro = '" & Trim(Job) & "') Order By uid desc", 1)
        Return ds
    End Function
    Public Function Sailing_Last_Nro() As Integer
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT TOP (1) Sailing_num AS Last_nro FROM LinDef Where Line_Number = " & md.GL_Company, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Last_nro")
        Else
            Return 0
        End If
    End Function

    ' ------- Change done on 10/11/2022, new function to get all BLs for a container
    Public Function Sailing_Get_BLs(ByVal Sailing As String) As DataSet
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        strSQL = "SELECT bk.BOOKING_NUMBER, bd.seq_number, ISNULL(bl.BL_NUMBER, '') AS BL_Number
                    FROM dbo.Bookings_Headline AS bk INNER JOIN
                         dbo.Booking_Detail AS bd ON bk.BOOKING_NUMBER = bd.BOOKING_NUMBER INNER JOIN
                         dbo.BLDTL AS bl ON bl.Booking_Number = bd.BOOKING_NUMBER AND bl.BK_Seq = bd.seq_number
                    WHERE (bk.Sailing_nro = '" & Trim(Sailing) & "') 
                    ORDER BY BL_Number"
        ' ------ Change done on 10/17/2022, add Shipper, Consignee, etc.
        strSQL = "SELECT bk.BOOKING_NUMBER, ISNULL(bl.BL_NUMBER, '') AS BL_Number, 
                      isnull((Select Top 1 Vessel_Name From Sailing_Master Where bk.Sailing_nro = Sailing_Master.Sailing_nro and (bk.Port_Loading = Sailing_Master.port_loading or bk.Port_Transshipment = Sailing_Master.port_Loading)),'') as Vessel,
                                       isnull((Select Top 1 Voyage_nro From Sailing_Master Where bk.Sailing_nro = Sailing_Master.Sailing_nro and (bk.Port_Loading = Sailing_Master.port_loading or bk.Port_Transshipment = Sailing_Master.port_Loading)),'') as Voyage,
	                                   isnull((Select Top 1 Format(Est_Departure,'MM/dd/yyyy') as Est_Departure From Sailing_Master Where bk.Sailing_nro = Sailing_Master.Sailing_nro and (bk.Port_Loading = Sailing_Master.port_loading or bk.Port_Transshipment = Sailing_Master.port_Loading)),'') as Departure,
                      sh.Company_name as Shipper,
                      cg.company_name as Consignee, fw.Company_name as FWDR, nf.Company_name as Notify
                   FROM dbo.Bookings_Headline AS bk INNER JOIN
                        dbo.Booking_Detail AS bd ON bk.BOOKING_NUMBER = bd.BOOKING_NUMBER INNER JOIN
                        dbo.BLDTL AS bl ON bl.Booking_Number = bd.BOOKING_NUMBER AND bl.BK_Seq = bd.seq_number Inner Join
		                BillOfLoadings as b on b.BL_NUMBER = bl.BL_number Inner Join
		                CM_System as sh on sh.COMPANY_NUMBER = b.SHIPPER_NUMBER and sh.LOCATION_NUMBER = b.Shipper_Loc Inner Join
		                CM_System as cg on cg.COMPANY_NUMBER = b.Cons_NUMBER and cg.LOCATION_NUMBER = b.Cons_Loc inner join
	                 CM_System as fw on fw.COMPANY_NUMBER = b.FWDR_NUMBER and fw.LOCATION_NUMBER = b.FWDR_Loc inner join
	                    CM_System as nf on nf.COMPANY_NUMBER = b.Notify_NUMBER and nf.LOCATION_NUMBER = b.Notify_Loc 
                 WHERE (bk.Sailing_nro = '" & Trim(Sailing) & "') 
                   ORDER BY BL_Number"
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds
        Else
            Return ds
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Public Function Sailing_Get_Vessel(ByVal Sailing As String) As String
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT Top 1 Isnull(Vessel_Name,'') as Vessel FROM Sailing_Master WHERE (Sailing_nro = '" & Trim(Sailing) & "' and Leg_nro = 1) Order By uid", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Vessel")
        Else
            Return ""
        End If
    End Function

    Public Function Sailing_Posted(ByVal Sailing As String) As Boolean
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT Top 1 Isnull(Posted,'') as Posted FROM Sailing_Master WHERE Sailing_nro = '" & Trim(Sailing) & "'", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            If Trim(ds.Tables(0).Rows(0).Item("Posted")) = "P" Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function
    Public Function Sailing_Get_Voyage(ByVal Sailing As String) As String
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT Top 1 Isnull(Voyage_Nro,'') as Voyage FROM Sailing_Master WHERE (Sailing_nro = '" & Trim(Sailing) & "' and Leg_nro = 1) Order By uid", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Voyage")
        Else
            Return ""
        End If
    End Function
    Public Function Sailing_Get_PortLoading(ByVal Sailing As String) As Integer
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT Top 1 Isnull(Port_Loading,'') as Port_Loading FROM Sailing_Master WHERE (Sailing_nro = '" & Trim(Sailing) & "' and Leg_nro = 1) Order By uid", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Port_Loading")
        Else
            Return 0
        End If
    End Function

    Public Function Sailing_Get_Departure(ByVal Sailing As String, ByVal PortL As Integer) As String
        Dim ds As New DataSet
        strSQL = "SELECT Top 1 Format(Est_Departure,'MM/dd/yyyy') as Departure FROM Sailing_Master WHERE (Sailing_nro = '" & Trim(Sailing) & "') and (Port_Loading = " & PortL & ") Order by Leg_nro"
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Departure")
        Else
            Return ""
        End If
    End Function

    Public Function Sailing_Get_Next_Voyage_of_Vessel(ByVal Vessel As String) As Integer
        Dim nVoyage As Integer = 0
        Dim vVoyage As String = ""
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT Top 1 Isnull(Voyage_Nro,'') as Voyage FROM Sailing_Master WHERE (Vessel_Name = '" & Trim(Vessel) & "') Order By Sailing_nro Desc", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            vVoyage = Replace(ds.Tables(0).Rows(0).Item("Voyage"), "S", "")
            vVoyage = Replace(vVoyage, "N", "")
            If IsNumeric(vVoyage) Then
                nVoyage = Val(vVoyage)
            End If
            Return nVoyage + 1
        Else
            Return 0
        End If
    End Function
#End Region

#Region "Vessel"
    Public Function Vessel_Audit_BY_Vessel(ByVal Vessel_Name As String) As DataSet
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT Created_Date, Created_TIME, CREATED_BY, DESCRIPTION, Vessel_Name, Old_Value  FROM VesselAudit
                         WHERE  (Vessel_Name = '" & Trim(Vessel_Name) & "') Order By uid desc", 1)
        Return ds
    End Function

    Public Function Vessel_Crowley(ByVal Vessel_Name As String) As String
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT VESSEL_NAME, OWNER_NUM, isnull(OWNER_LOC,0) as Owner_Loc, Owner_name
                                            FROM  (SELECT VESSEL_NAME, OWNER_NUM, OWNER_LOC, Owner_name
                                                     FROM (SELECT v.VESSEL_NAME, v.NATIONALITY, v.REGISTRY, v.LLOYDS_CODE, v.COUNTRY_CODE, v.OWNER_NUM, v.OWNER_LOC, c.COMPANY_NAME AS Owner_name
                                                              FROM dbo.Vessels AS v INNER JOIN
                                                                   dbo.CM_System AS c ON c.COMPANY_NUMBER = v.OWNER_NUM AND c.LOCATION_NUMBER = isnull(v.OWNER_LOC,0)
                                                              WHERE (v.LINE_NUMBER = 1)) AS T1
                                                       WHERE        (Owner_name LIKE '%CROWLEY%')) AS T2
                                            WHERE (VESSEL_NAME = '" & Trim(Vessel_Name) & "')", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            ds = Nothing
            Return "Y"
        Else
            ds = Nothing
            Return "N"
        End If
    End Function
#End Region

#Region "CMS Name File"
    Public Function CMS_GDZ_NameFile(ByVal Acc As Integer, ByVal Loc As Integer) As DataSet
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT  * FROM  CM_System where Company_Number = " & Trim(Acc) & " and Location_Number = " & Loc, 1)
        Return ds
    End Function
    Public Function CMS_Address(ByVal Acc As Integer, ByVal Loc As Integer) As String
        Dim vAddress As String = ""
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT * FROM CM_System where Company_Number = " & Trim(Acc) & " and Location_Number = " & Loc, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            vAddress = md.Address_ansamble(Trim(ds.Tables(0).Rows(0).Item("street")) & " " & Trim(ds.Tables(0).Rows(0).Item("suite")), Trim(ds.Tables(0).Rows(0).Item("city")), Trim(ds.Tables(0).Rows(0).Item("state")), Trim(ds.Tables(0).Rows(0).Item("zip")), Trim(ds.Tables(0).Rows(0).Item("country")), Trim(ds.Tables(0).Rows(0).Item("phone")))
            ' md.v_L1 = FormatStrLine(Trim(vAddress).ToString)
            'vAddress = md.v_L1
        End If
        Return vAddress
    End Function
    Public Function CMS_NameFile(ByVal Name As String) As DataSet
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT Distinct Top(30) company_name as name,Company_number as code  FROM  CM_System where Company_Name like '" & Trim(Replace(Name, "'", "''")) & "%' and isnull(SDN_List,'') = '' order by company_name", 1)
        Return ds
    End Function
    Public Function CMS_Get_Contact_Phone_email(ByVal acc As Integer, ByVal Program_Type As String) As DataSet
        strSQL = "SELECT isnull(Contact_Name,'') Contact, isnull(Phone,'') as Phone, isnull(email,'') as email FROM  dbo.CMSeMail WHERE (CMSNUM = " & acc & ") AND ([Document] = '" & Trim(Program_Type) & "')"
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, strSQL, 1)
        Return ds
    End Function
    Public Function CMS_Get_Street_City_State_ZIP_Contact_Phone(ByVal acc As Integer, ByVal Loc As Integer) As DataSet
        strSQL = "SELECT rtrim(isnull(Street,'')) + '  ' + rTrim(isnull(Suite,'')) as Street, isnull(City,'') as City, isnull(State,'') as State, isnull(ZIP,'') as ZIP, isnull(Phone,'') as Phone, isnull(Contact,'') as Contact FROM  dbo.CM_System WHERE (Company_number = " & acc & ") AND (Location_Number = " & Loc & ")"
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, strSQL, 1)
        Return ds
    End Function
    Public Function CMS_Name(ByVal number As Integer) As String
        Dim vName As String = ""
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT  company_name as name,Company_number as code  FROM  CM_System where Company_Number = " & number, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Name")
        Else
            Return ""
        End If
    End Function
    Public Function CMS_Get_SDN_List(ByVal number As Integer) As String
        Dim vName As String = ""
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT ISNULL(SDN_List, N'') as name,Company_number as code  FROM  CM_System where Company_Number = " & number & " and Location_number = 0", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            If Len(Trim(ds.Tables(0).Rows(0).Item("Name"))) > 0 Then
                Return ds.Tables(0).Rows(0).Item("Name")
                ds = Nothing
            Else
                Return ""
                ds = Nothing
            End If
        Else
            Return ""
            ds = Nothing
        End If
    End Function

    ' ------- Change done on 08/31/2022 
    Public Function CMS_Get_email(ByVal Type As String, ByVal number As Integer) As String
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT Top 1 isnull(email,'') as email FROM CMSeMail WHERE CMSNUM = " & number & " and ([Document] = '" & Trim(Type) & "')", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            If Len(Trim(ds.Tables(0).Rows(0).Item("eMail"))) > 0 Then
                Return Trim(ds.Tables(0).Rows(0).Item("eMail"))
                ds = Nothing
            Else
                Return ""
                ds = Nothing
            End If
        Else
            Return ""
            ds = Nothing
        End If
    End Function

    Public Function CMS_Get_Booking_Trucker_emails(ByVal number As Integer) As DataSet
        strSQL = "SELECT isnull(email,'') as email FROM CMSeMail WHERE (CMSNUM = " & md.Trucker_D & " or CMSNUM = " & number & ")  and ([Document] = 'BOOKING')"
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        Return ds
    End Function

    ' ------- Change done on 08/31/2022 
    Public Function CMS_Get_phone(ByVal Type As String, ByVal number As Integer) As String
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT Top 1 isnull(phone,'') as phone FROM CMSeMail WHERE CMSNUM = " & number & " and ([Document] = '" & Trim(Type) & "')", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            If Len(Trim(ds.Tables(0).Rows(0).Item("phone"))) > 0 Then
                Return Trim(ds.Tables(0).Rows(0).Item("phone"))
                ds = Nothing
            Else
                Return ""
                ds = Nothing
            End If
        Else
            Return ""
            ds = Nothing
        End If
    End Function

    ' ------- Change done on 10/11/2022, new function to get all BLs for a container
    Public Function CMS_Get_BLs(ByVal dFrom As Date, ByVal dTo As Date, ByVal CMS_Code As Integer) As DataSet
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        strSQL = "SELECT b.BL_NUMBER, b.BL_DATE, 
                       bk.Sailing_nro,
	                   isnull((Select Top 1 Vessel_Name From Sailing_Master Where bk.Sailing_nro = Sailing_Master.Sailing_nro and (bk.Port_Loading = Sailing_Master.port_loading or bk.Port_Transshipment = Sailing_Master.port_Loading)),'') as Vessel,
                       isnull((Select Top 1 Voyage_nro From Sailing_Master Where bk.Sailing_nro = Sailing_Master.Sailing_nro and (bk.Port_Loading = Sailing_Master.port_loading or bk.Port_Transshipment = Sailing_Master.port_Loading)),'') as Voyage,
	                   isnull((Select Top 1 Format(Est_Departure,'MM/dd/yyyy') as Est_Departure From Sailing_Master Where bk.Sailing_nro = Sailing_Master.Sailing_nro and (bk.Port_Loading = Sailing_Master.port_loading or bk.Port_Transshipment = Sailing_Master.port_Loading)),'') as Departure,
	                   sh.Company_name as Shipper, cg.company_name as Consignee, fw.Company_name as FWDR, nf.Company_name as Notify
                   FROM dbo.BillOfLoadings as b Inner Join
                       Bookings_Headline as bk on b.Booking_Number = bk.Booking_Number Inner Join
                       CM_System as sh on sh.COMPANY_NUMBER = b.SHIPPER_NUMBER and sh.LOCATION_NUMBER = b.Shipper_Loc inner join
	                   CM_System as cg on cg.COMPANY_NUMBER = b.Cons_NUMBER and cg.LOCATION_NUMBER = b.Cons_Loc inner join
	                   CM_System as fw on fw.COMPANY_NUMBER = b.FWDR_NUMBER and fw.LOCATION_NUMBER = b.FWDR_Loc inner join
	                   CM_System as nf on nf.COMPANY_NUMBER = b.Notify_NUMBER and nf.LOCATION_NUMBER = b.Notify_Loc 
                 WHERE  (Format(b.BL_Date,'yyyy-MM-dd') Between '" & dFrom & "' and '" & dTo & "') 
		                and  (b.Shipper_Number = " & CMS_Code & " or b.Cons_Number = " & CMS_Code & " or b.Fwdr_Number = " & CMS_Code & " or b.notify_number = " & CMS_Code & ") 
                ORDER BY b.BL_Number"
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds
        Else
            Return ds
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Public Function CMS_Number_x_Name_(ByVal name As String) As Integer
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT top 1 company_name as name,Company_number as code FROM CM_System where Company_Name = '" & Trim(Replace(name, "'", "''")) & "'", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Code")
        Else
            Return 0
        End If
    End Function
    Public Function CMS_Number_x_Name_lIKE(ByVal name As String) As Integer
        Dim ds As New DataSet
        strSQL = "SELECT Top 1 company_name as name,Company_number as code FROM CM_System where SUBSTRING(Company_Name,1,30) LIKE '" & Trim(Replace(name, "'", "''")) & "%'"
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Code")
        Else
            Return 0
        End If
    End Function
    Public Function CMS_Credit(ByVal Customer_number As Integer) As Boolean
        Dim vName As String = ""
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT Credit_Limit FROM CUSTFL where Company_number = 1 and Customer_Number = " & Customer_number & " and credit_Limit = 0", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function CMS_NameFile_All() As DataSet
        Dim vName As String = ""
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT Distinct company_name,Company_number,isnull(Location_Number,0) as Location_Number FROM  CM_System  where not Company_name like '%DELETED%' order by company_name", 1)
        Return ds
    End Function
    Public Function Terminal_Master()
        Dim ds As New DataSet
        'strSQL = "SELECT Company_name as Terminal_Name,company_number as Terminal_Number FROM CM_System Where (ISNULL(Port_Yard, '') = 'Y') and Location_Number = 0 Order by Company_Name"
        strSQL = "SELECT port_name as Terminal_name,Port_number as Terminal_Number FROM  Ports WHERE (ISNULL(PortPlace, 'N') = 'Y') Order by port_name"
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        Return ds
    End Function
    Function Address_ansamble(ByVal street As String, ByVal city As String, ByVal state As String, ByVal zip As String, ByVal country As String, ByVal phone As String) As String
        Dim vAddress As String = ""
        If street.Length > 0 Then
            vAddress = Trim(street)
        End If

        'If Suite.Length > 0 Then
        '    vAddress = Trim(vAddress) & ", " & Trim(Suite)
        'End If
        If city.Length > 0 Then
            If Len(Trim(vAddress)) > 0 Then
                vAddress = Trim(vAddress) & Chr(13) & Chr(10) & Trim(city)
            Else
                vAddress = Trim(city)
            End If
        End If

        If state.Length > 0 Then
            If Len(Trim(vAddress)) > 0 Then
                vAddress = Trim(vAddress) & ", " & Trim(state)
            Else
                vAddress = Trim(state)
            End If
        End If
        If zip.Length > 0 Then
            If Len(Trim(vAddress)) > 0 Then
                vAddress = Trim(vAddress) & Chr(13) & Chr(10) & Trim(zip)
            Else
                vAddress = Trim(zip)
            End If
        End If
        If country.Length > 0 Then
            If Len(Trim(vAddress)) > 0 Then
                vAddress = Trim(vAddress) & Chr(13) & Chr(10) & Trim(country)
            Else
                vAddress = Trim(country)
            End If
        End If
        If phone.Length > 0 Then
            If Len(Trim(vAddress)) > 0 Then
                vAddress = Trim(vAddress) & Chr(13) & Chr(10) & Trim(phone)
            Else
                vAddress = Trim(phone)
            End If
        End If
        Return Trim(vAddress)
    End Function
    Public Function CMS_Desc(ByVal Acc As Integer) As String
        strSQL = "Select isnull(Description,'') as Description FROM CM_System WHERE Company_Number = " & Acc
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return Trim(ds.Tables(0).Rows(0).Item("Description"))
        Else
            Return ""
        End If
        ds = Nothing
    End Function

#End Region

#Region "Line"
    Public Function Line_USCS_USER_CODE(ByVal Line As Integer) As String
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT Rtrim(USCS_USER_CODE) as USCS_User_Code FROM   dbo.LINDEF Where Line_Number = " & Line, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("USCS_User_Code")
        Else
            Return ""
        End If
    End Function
#End Region

#Region "TIR (DockReceipts)"
    Public Function TIR_BY_BK(ByVal BK As Integer) As DataSet
        Dim BK_Transshipment As Integer = 0
        Dim ds As New DataSet
        strSQL = "Select Booking_Number,isnull(BK_Transshipment,0) as BK_Transshipment From Bookings_Headline Where Booking_Number = " & BK
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0).Item("BK_transshipment") > 0 And (ds.Tables(0).Rows(0).Item("BK_transshipment") <> ds.Tables(0).Rows(0).Item("Booking_Number")) Then
                BK_Transshipment = ds.Tables(0).Rows(0).Item("BK_transshipment")
            End If
        End If
        If BK_Transshipment > 0 Then
            strSQL = "Select Voyage, South_North, DR_STEVEDORING, DR_NUMBER, MOVEMENT_TYPE, Format(DR_DATE,'MM/dd/yyyy') as DR_Date, MOVEMENT_TIME, CREATION_TIME, format(CREATION_DATE,'MM/dd/yyyy') as Creation_Date, CREATED_BY, TOT_WEIGHT, TOT_MEASURE, REM_WEIGHT, 
                          REM_MEASURE, Container, Cont_1, Cont_2, Cont_3, Chassis, Chas_1, Chas_2, Chas_3, Sailing_nro, t.DC_FLAG, t.BOOKING_NUMBER, BOOKING_SEQ, LINE_NUMBER, SEAL_NUMBER, EQUIPMENT_TYPE, INVENTORY_FLAG,
                          TEMPERATURE, WHERE_CODE, WHERE_NUM, WHERE_LOC, Where_name, Where_Desc, Where_desc_r, GenSet, GenSet_1, GenSet_2, GenSet_3, SHIPPER_EXTRA, FWDR_EXTRA, CONS_EXTRA, NOTIFY_EXTRA, t.ID, 
                         Fuel_level, Last_BK_TIR 
                          From TIR_Cnt_Transfer    as t Inner Join 
			              dbo.Booking_Detail as d on d.Booking_Number = t.Booking_Number 
                      where (t.Booking_number='" & BK & "' or t.Booking_Number = " & BK_Transshipment & ")  and d.seq_Number = t.Booking_Seq Order by Container, Format(DR_DATE,'yyyy-MM-dd') desc, Movement_Time Desc"
        Else
            strSQL = "Select Voyage, South_North, DR_STEVEDORING, DR_NUMBER, MOVEMENT_TYPE, Format(DR_DATE,'MM/dd/yyyy') as DR_Date, MOVEMENT_TIME, CREATION_TIME, format(CREATION_DATE,'MM/dd/yyyy') as Creation_Date, CREATED_BY, TOT_WEIGHT, TOT_MEASURE, REM_WEIGHT, 
                         REM_MEASURE, Container, Cont_1, Cont_2, Cont_3, Chassis, Chas_1, Chas_2, Chas_3, Sailing_nro, t.DC_FLAG, t.BOOKING_NUMBER, BOOKING_SEQ, LINE_NUMBER, SEAL_NUMBER, EQUIPMENT_TYPE, INVENTORY_FLAG,
                          TEMPERATURE, WHERE_CODE, WHERE_NUM, WHERE_LOC, Where_name, Where_Desc, Where_desc_r, GenSet, GenSet_1, GenSet_2, GenSet_3, SHIPPER_EXTRA, FWDR_EXTRA, CONS_EXTRA, NOTIFY_EXTRA, t.ID, 
                         Fuel_level, Last_BK_TIR from TIR_Cnt_Transfer  as t Inner Join 
			           dbo.Booking_Detail as d on d.Booking_Number = t.Booking_Number and d.seq_Number = t.Booking_Seq where t.Booking_number='" & BK & "'  Order by Container, Format(DR_DATE,'yyyy-MM-dd') desc, Movement_Time Desc"
        End If
        ds.Clear()
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        Return ds
    End Function

    Public Function TIR_BY_BK_Old(ByVal BK As Integer) As DataSet
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT isnull(BOOKING_NUMBER,0) as BK, isnull(seq_number,1) as seq, 
                             isnull(CONTAINERS,0) as Qty, isnull(CONTAINER_TYPE,'') as Cont_type, 
                             isnull(DESCRIPTION,'') as Description, isnull(DC_Flag,'') as Flag,
                             isnull(Temp_From,'') as Temp_From,isnull(Temp_To,'') as Temp_to, isnull(Temp_Type,'') as Temp_Type,
	                         isnull((Select Top 1 Cont_1 From TIR_Cnt_Transfer Where TIR_Cnt_Transfer.Booking_number = Booking_Detail.Booking_Number and TIR_Cnt_Transfer.DR_Number = Booking_Detail.TIR Order By ID Desc),'') as Cont_1,
	                         isnull((Select Top 1 Cont_2 From TIR_Cnt_Transfer Where TIR_Cnt_Transfer.Booking_number = Booking_Detail.Booking_Number and TIR_Cnt_Transfer.DR_Number = Booking_Detail.TIR Order By ID Desc),'') as Cont_2,
	                         isnull((Select Top 1 Cont_3 From TIR_Cnt_Transfer Where TIR_Cnt_Transfer.Booking_number = Booking_Detail.Booking_Number and TIR_Cnt_Transfer.DR_Number = Booking_Detail.TIR Order By ID Desc),'') as Cont_3,
	                         isnull((Select Top 1 Movement_Type From TIR_Cnt_Transfer Where TIR_Cnt_Transfer.Booking_number = Booking_Detail.Booking_Number and TIR_Cnt_Transfer.DR_Number = Booking_Detail.TIR Order By ID Desc),'') as Movement_Type,
	                         isnull((Select Top 1 Chas_1 From TIR_Cnt_Transfer Where TIR_Cnt_Transfer.Booking_number = Booking_Detail.Booking_Number and TIR_Cnt_Transfer.DR_Number = Booking_Detail.TIR Order By ID Desc),'') as CHas_1,
	                         isnull((Select Top 1 Chas_2 From TIR_Cnt_Transfer Where TIR_Cnt_Transfer.Booking_number = Booking_Detail.Booking_Number and TIR_Cnt_Transfer.DR_Number = Booking_Detail.TIR Order By ID Desc),'') as CHas_2,
	                         isnull((Select Top 1 Chas_3 From TIR_Cnt_Transfer Where TIR_Cnt_Transfer.Booking_number = Booking_Detail.Booking_Number and TIR_Cnt_Transfer.DR_Number = Booking_Detail.TIR Order By ID Desc),'') as CHas_3
                        FROM Booking_Detail
                       WHERE (BOOKING_NUMBER = " & BK & ")
                      ORDER BY seq_number", 1)
        Return ds
    End Function

    Public Function TIR_Last(ByVal Yard As Integer) As Integer
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim nTIR As Integer = 0
        Dim ds As New DataSet
        Dim strSQL = "SELECT Top 1 DR_NUMBER FROM TIR_Cnt_Transfer WHERE (DR_STEVEDORING = " & Yard & " AND (DR_NUMBER < 20000000) ) ORDER BY DR_NUMBER DESC"
        ds = ws.GetDataset(md.strConnect, "SELECT Top 1 DR_NUMBER FROM TIR_Cnt_Transfer WHERE (DR_STEVEDORING = " & Yard & ") ORDER BY DR_NUMBER DESC", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            nTIR = ds.Tables(0).Rows(0).Item("DR_Number")
            Return nTIR
        Else
            Return 10000000
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Public Function TIR_First(ByVal Yard As Integer) As Integer
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT Top 1 DR_NUMBER FROM TIR_Cnt_Transfer WHERE (DR_STEVEDORING = " & Yard & ")ORDER BY DR_NUMBER", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("DR_Number")
        Else
            Return 10000000
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Public Function TIR_Audit_BY_TIR(ByVal TIR As Integer) As DataSet
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT TIR, Description, Created_ON, Created_By, Old_value FROM TIR_Journal
                         WHERE  (TIR =" & TIR & ") Order By uid desc", 1)
        Return ds
    End Function

    Public Function DR_Last_All(ByVal Warehouse As Integer) As Integer
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim nDR As Integer = 0
        Dim ds As New DataSet
        Dim strSQL = "SELECT Top 1 DR_NUMBER FROM DockReceipts ORDER BY DR_NUMBER DESC"
        ds = ws.GetDataset(md.strConnect, "SELECT Top 1 DR_NUMBER FROM DockReceipts where DR_Stevedoring = " & Warehouse & " ORDER BY DR_Number DESC", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            nDR = ds.Tables(0).Rows(0).Item("DR_Number")
            Return nDR
        Else
            Return 1000
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Public Function DR_Number(ByVal DR As String) As Integer
        Dim vDR As String = ""
        Dim nDR As Integer = 0
        Dim pos As Integer = 0
        DR = LTrim(Trim(Replace(DR, "%U%", "")))
        pos = InStr(DR, "-")
        If pos > 0 Then
            vDR = Mid(DR, 1, pos - 1)
            If IsNumeric(vDR) Then
                nDR = Val(vDR)
            Else
                MsgBox("DR# must be numeric")
            End If
        End If
        Return nDR
    End Function

    Public Function DR_Unit(ByVal DR As String) As Integer
        Dim nUnit As Integer = 0
        Dim pos As Integer = 0
        DR = LTrim(Trim(Replace(DR, "%U%", "")))
        Dim nLen As Integer = Len(Trim(DR))
        pos = InStr(DR, "-")
        If pos > 0 Then
            nUnit = Val(Mid(DR, pos + 1, nLen - pos))
        End If
        Return nUnit
    End Function
    Public Function DR_Last(ByVal Created_By As String) As Integer
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim nDR As Integer = 0
        Dim ds As New DataSet
        Dim strSQL = "SELECT Top 1 DR_NUMBER FROM DockReceipts WHERE Created_By = '" & Trim(Created_By) & "') ORDER BY DR_NUMBER DESC"
        ds = ws.GetDataset(md.strConnect, "SELECT Top 1 DR_NUMBER FROM DockReceipts WHERE (Created_By = '" & Trim(Created_By) & "') ORDER BY id DESC", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            nDR = ds.Tables(0).Rows(0).Item("DR_Number")
            Return nDR
        Else
            Return 1000
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Public Function DR_Get_Project(ByVal Warehouse As Integer, ByVal DR As Integer) As String
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim ds As New DataSet
        Dim strSQL = "SELECT isnull(Project,'') as Project FROM DockReceipts WHERE DR_Stevedoring = " & Warehouse & " and DR_Number = " & DR
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Project")
        Else
            Return ".N/A."
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Public Function DR_Get_Status_Code(ByVal Status_Desc As String) As String
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim ds As New DataSet
        Dim strSQL = "SELECT isnull(Code,'') as Code FROM DR_Status WHERE Description = '" & Trim(Status_Desc) & "'"
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return Trim(ds.Tables(0).Rows(0).Item("Code"))
        Else
            Return ""
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Public Function DR_Get_Pro_Number(ByVal Warehouse As Integer, ByVal DR As Integer) As String
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim ds As New DataSet
        Dim strSQL = "SELECT isnull(Pro_Number,'') as Pro_Number FROM DockReceipts WHERE DR_Stevedoring = " & Warehouse & " and DR_Number = " & DR
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Pro_Number")
        Else
            Return ""
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Public Function DR_Next_Seq(ByVal Warehouse As Integer, ByVal DR As Integer) As Integer
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim nDR As Integer = 0
        Dim ds As New DataSet
        Dim strSQL = "SELECT count(*) as Tot_Lin FROM dbo.DockReceipt_Pkgs WHERE DR_STEVEDORING = " & Warehouse & " and (DR_NUMBER = " & DR & ")"
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0).Item("Tot_Lin") = 1 Then
                Return 1
            Else


            End If
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function


    Public Function DR_ReOrder_Lines(ByVal Warehouse As Integer, ByVal DR As Integer) As Boolean
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim ds As New DataSet
        strSQL = "Select DR_Number From (SELECT ROW_NUMBER() OVER (ORDER BY seq_Number) AS Line_number,   ID, DR_STEVEDORING, DR_NUMBER, DR_Seq, SEQ_NUMBER
                                                 FROM dbo.DockReceipts_Details WHERE DR_STEVEDORING = " & Warehouse & " and (DR_NUMBER = " & DR & ")) as T1 Where Line_Number <> Seq_Number"
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        ' ------- Chack need to reorder
        If ds.Tables(0).Rows.Count > 0 Then
            Dim ds_dtl As New DataSet
            strSQL = "SELECT ROW_NUMBER() OVER (ORDER BY seq_Number) AS Line_number,   ID, DR_STEVEDORING, DR_NUMBER, DR_Seq, SEQ_NUMBER
                        FROM dbo.DockReceipts_Details
                       WHERE DR_STEVEDORING = " & Warehouse & " and (DR_NUMBER = " & DR & ") ORDER BY DR_STEVEDORING, DR_NUMBER, SEQ_NUMBER "
            ds_dtl = ws.GetDataset(md.strConnect, strSQL, 1)
            If ds_dtl.Tables(0).Rows.Count > 0 Then
                Dim j As Integer = 0
                Dim nLin As Integer = 0
                For j = 0 To ds_dtl.Tables(0).Rows.Count - 1
                    nLin = ds_dtl.Tables(0).Rows(j).Item("Line_Number")
                    ' ------- Update Unit
                    strSQL = "Update dbo.DockReceipt_Pkgs set DR_Seq = " & nLin & " WHERE DR_Stevedoring = " & Warehouse & " and DR_NUMBER = '" & DR & "' AND DR_SEQ = " & ds_dtl.Tables(0).Rows(j).Item("seq_Number")
                    eResp = ws.ExecSQL(strConnect, strSQL)
                    ' ------- Update Hazmat
                    strSQL = "Update dbo.DRDC set DR_Seq = " & nLin & " WHERE DR_Stevedoring = " & Warehouse & " and DR_NUMBER = '" & DR & "' AND DR_Seq = " & ds_dtl.Tables(0).Rows(j).Item("seq_Number")
                    eResp = ws.ExecSQL(strConnect, strSQL)
                    ' ------- Update Detail
                    strSQL = "Update dbo.DockReceipts_Details set seq_Number = " & nLin & ", DR_Seq = " & nLin & " WHERE ID = " & ds_dtl.Tables(0).Rows(j).Item("ID")
                    eResp = ws.ExecSQL(strConnect, strSQL)
                Next
            End If

            ' ------ ReOrder x Unit
            ds_dtl.Clear()
            strSQL = "SELECT ROW_NUMBER() OVER (ORDER BY DockReceipt_Pkg_Unit_id) AS Unit_number,  uid, DR_STEVEDORING, DR_NUMBER, DR_Seq, DockReceipt_Pkg_Unit_id
                        FROM dbo.DockReceipt_Pkgs
                       WHERE DR_STEVEDORING = " & Warehouse & " and (DR_NUMBER = " & DR & ") ORDER BY DR_STEVEDORING, DR_NUMBER, DR_Seq, DockReceipt_Pkg_Unit_id"
            ds_dtl = ws.GetDataset(md.strConnect, strSQL, 1)
            If ds_dtl.Tables(0).Rows.Count > 0 Then
                For j = 0 To ds_dtl.Tables(0).Rows.Count - 1
                    ' ------- Update Units
                    strSQL = "Update DockReceipt_Pkgs set DockReceipt_Pkg_Unit_id = " & ds_dtl.Tables(0).Rows(j).Item("Unit_Number") & " WHERE uid = " & ds_dtl.Tables(0).Rows(j).Item("uid")
                    eResp = ws.ExecSQL(strConnect, strSQL)
                Next
            End If

            ' ------ ReOrder x HazMat : seq_Number
            ds_dtl.Clear()
            strSQL = "SELECT ROW_NUMBER() OVER (ORDER BY DR_STEVEDORING, DR_NUMBER, DR_SEQ, Unit_Nro, SEQ_NUMBEr) AS row_number,  uid, DR_STEVEDORING, DR_NUMBER, DR_Seq, Unit_Nro, Seq_Number
                        FROM dbo.DRDC
                       WHERE DR_STEVEDORING = " & Warehouse & " and (DR_NUMBER = " & DR & ") ORDER BY DR_STEVEDORING, DR_NUMBER, DR_SEQ, Unit_Nro, SEQ_Number"
            ds_dtl = ws.GetDataset(md.strConnect, strSQL, 1)
            If ds_dtl.Tables(0).Rows.Count > 0 Then
                For j = 0 To ds_dtl.Tables(0).Rows.Count - 1
                    ' ------- Update Units
                    strSQL = "Update DRDC set Seq_Number = " & ds_dtl.Tables(0).Rows(j).Item("row_Number") & " WHERE uid = " & ds_dtl.Tables(0).Rows(j).Item("uid")
                    eResp = ws.ExecSQL(strConnect, strSQL)
                Next
            End If

            Return True
        Else
            Return False
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Public Function DR_Line_To_Update_L_W_H_Weight_Picture(ByVal Warehouse As Integer, ByVal DR As Integer) As Integer
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim nDR As Integer = 0
        Dim ds As New DataSet
        strSQL = "SELECT TOP (1) DockReceipt_Pkg_Unit_id as nLin FROM dbo.DockReceipt_Pkgs WHERE  DR_STEVEDORING = " & Warehouse & " and (DR_NUMBER = " & DR & ") AND (WEIGHT = 0) AND (MEASURE = 0) AND (UNIT_LENGTH = 0) AND (UNIT_HEIGHT = 0) AND (UNIT_WIDTH = 0) ORDER BY DockReceipt_Pkg_Unit_id"
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("nLin")
        Else
            'Return 0
            strSQL = "SELECT TOP (1) DockReceipt_Pkg_Unit_id as nLin FROM dbo.DockReceipt_Pkgs WHERE  DR_STEVEDORING = " & Warehouse & " and (DR_NUMBER = " & DR & ") ORDER BY DockReceipt_Pkg_Unit_id"
            ds = ws.GetDataset(md.strConnect, strSQL, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                Return 1
            Else
                Return 0
            End If
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Public Function DR_get_Scale(ByVal MachineName As String) As String
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim ds As New DataSet
        Dim strSQL = "SELECT isnull(Scale,'') as Scale FROM dbo.Version_Machine WHERE (MachineName = '" & Trim(MachineName) & "')"
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Scale")
        Else
            Return ""
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Public Function DR_get_BL(ByVal Warehouse As Integer, ByVal DR As Integer) As String
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim ds As New DataSet
        Dim strSQL = "SELECT isnull(BL_Number,'') as BL_Number FROM DockReceipt_Pkgs WHERE DR_STEVEDORING = " & Warehouse & " and DR_NUMBER = " & DR
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("BL_Number")
        Else
            Return ""
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Public Function DR_get_BL_Lin(ByVal Warehouse As Integer, ByVal DR As Integer, ByVal Unit As Integer) As Integer
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim ds As New DataSet
        Dim strSQL = "SELECT isnull(BL_Number,'') as BL_Number, BL_Lin FROM DockReceipt_Pkgs WHERE DR_STEVEDORING = " & Warehouse & " and DR_NUMBER = " & DR & " and DockReceipt_Pkg_Unit_id = " & Unit
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("BL_Lin")
        Else
            Return 0
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Public Function DR_get_Shipper(ByVal Warehouse As Integer, ByVal DR As Integer) As String
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim ds As New DataSet
        Dim strSQL = "SELECT r.FROM_NUMBER, r.FROM_LOC, r.From_Name, r.From_Desc, c.COMPANY_NAME AS Shipper
                        FROM dbo.DockReceipts AS r INNER JOIN
                             dbo.CM_System AS c ON c.COMPANY_NUMBER = r.FROM_NUMBER AND c.LOCATION_NUMBER = r.FROM_LOC
                        WHERE (r.DR_STEVEDORING = " & Warehouse & ") AND (r.DR_NUMBER = " & DR & ")"
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Shipper")
        Else
            Return ""
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Public Function DR_get_Consignee(ByVal Warehouse As Integer, ByVal DR As Integer) As String
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim ds As New DataSet
        Dim strSQL = "SELECT r.CONSIGNEE_NO, r.CONSIGNEE_LOC, c.COMPANY_NAME AS Consignee
                        FROM dbo.DockReceipts AS r INNER JOIN
                             dbo.CM_System AS c ON c.COMPANY_NUMBER = r.CONSIGNEE_NO AND c.LOCATION_NUMBER = r.CONSIGNEE_LOC
                       WHERE (r.DR_STEVEDORING = " & Warehouse & ") AND (r.DR_NUMBER = " & DR & ")"
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Consignee")
        Else
            Return ""
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Public Function DR_Count_Lines(ByVal Warehouse As Integer, ByVal DR As Integer) As Integer
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim nDR As Integer = 0
        Dim ds As New DataSet
        Dim strSQL = "SELECT Count(*) as T_Lin FROM dbo.DockReceipt_Pkgs WHERE  DR_STEVEDORING = " & Warehouse & " and (DR_NUMBER = " & DR & ")"
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("T_Lin")
        Else
            Return 0
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Public Function DR_Get_Tot_DR_BetweenDates(ByVal dFrom As Date, ByVal dTo As Date, ByVal Warehouse As Integer, ByVal nPortD As Integer) As Integer
        Dim ds As New DataSet
        If nPortD = 0 Or nPortD = 1 Then
            md.strSQL = "Select count(*) as Tot_Drs From
                        (SELECT DISTINCT h.DR_NUMBER
                                 FROM dbo.DockReceipts AS h INNER JOIN
                                      dbo.DockReceipts_Details AS d ON h.DR_STEVEDORING = d.DR_STEVEDORING AND h.DR_NUMBER = d.DR_NUMBER INNER JOIN
                                      dbo.DockReceipt_Pkgs AS u ON h.DR_STEVEDORING = u.DR_STEVEDORING AND h.DR_NUMBER = u.DR_NUMBER and d.seq_Number = u.DR_Seq INNER JOIN
                                      dbo.Ports AS pd ON pd.PORT_NUMBER = h.PORT_DISCH
                         WHERE (h.DR_DATE Between '" & dFrom & "' and '" & dTo & "') AND (h.DR_STEVEDORING = " & Warehouse & ")
                         Group By h.DR_NUMBER
                         ) as T1"
        Else
            md.strSQL = "Select count(*) as Tot_Drs From
                        (SELECT DISTINCT h.DR_NUMBER
                                 FROM dbo.DockReceipts AS h INNER JOIN
                                      dbo.DockReceipts_Details AS d ON h.DR_STEVEDORING = d.DR_STEVEDORING AND h.DR_NUMBER = d.DR_NUMBER INNER JOIN
                                      dbo.DockReceipt_Pkgs AS u ON h.DR_STEVEDORING = u.DR_STEVEDORING AND h.DR_NUMBER = u.DR_NUMBER and d.seq_Number = u.DR_Seq INNER JOIN
                                      dbo.Ports AS pd ON pd.PORT_NUMBER = h.PORT_DISCH
                         WHERE (h.DR_DATE Between '" & dFrom & "' and '" & dTo & "') AND (h.DR_STEVEDORING = " & Warehouse & " and PORT_DISCH = " & nPortD & ")
                         Group By h.DR_NUMBER
                         ) as T1"
        End If
        ds = ws.GetDataset(strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Tot_Drs")
        Else
            Return 0
        End If
        ds = Nothing
    End Function

    Public Function DR_Get_Tot_DR_BetweenDates_Shipper_Consignee(ByVal dFrom As Date, ByVal dTo As Date, ByVal Warehouse As Integer, ByVal nShipper As Integer, ByVal nCons As Integer) As Integer
        Dim ds As New DataSet
        md.strSQL = "Select count(*) as Tot_Drs From
                        (SELECT DISTINCT h.DR_NUMBER
                                 FROM dbo.DockReceipts AS h INNER JOIN
                                      dbo.DockReceipts_Details AS d ON h.DR_STEVEDORING = d.DR_STEVEDORING AND h.DR_NUMBER = d.DR_NUMBER INNER JOIN
                                      dbo.DockReceipt_Pkgs AS u ON h.DR_STEVEDORING = u.DR_STEVEDORING AND h.DR_NUMBER = u.DR_NUMBER and d.seq_Number = u.DR_Seq INNER JOIN
                                      dbo.Ports AS pd ON pd.PORT_NUMBER = h.PORT_DISCH
                         WHERE (h.DR_DATE Between '" & dFrom & "' and '" & dTo & "') AND (h.DR_STEVEDORING = " & Warehouse & ")"

        If nShipper <> 0 And nShipper <> 1 Then
            strSQL = Trim(strSQL) & " and (h.From_Number = " & nShipper & ")"
        End If
        If nCons <> 0 And nCons <> 1 Then
            strSQL = Trim(strSQL) & " and (h.Consignee_No = " & nCons & ")"
        End If
        strSQL = Trim(strSQL) & " Group By h.DR_NUMBER) as T1"

        ds = ws.GetDataset(strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Tot_Drs")
        Else
            Return 0
        End If
        ds = Nothing
    End Function
#End Region

    ' -----------------------------------------------------
#Region "Masters PKDesc, DC_Desc, CarDesc, Yard, Warehouse"
    Public Function Yard_Master() As DataSet
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT Company_name,company_number FROM CM_System Where (Yard = 'Y' or Yard='P') and Location_Number = 0 Order by Company_Name", 1)
        Return ds
    End Function
    Public Function Yard_OK(ByVal Yard As Integer) As Boolean
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT Company_name,company_number FROM CM_System Where Company_Number = " & Yard & " and (Yard = 'Y' or Yard='P') and Location_Number = 0 Order by Company_Name", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function Warehouse_Master() As DataSet
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT Company_name,company_number FROM CM_System Where Warehouse = 'W' and Location_number = 0 Order by Company_Name", 1)
        Return ds
    End Function

    Public Function Warehouse_port_x_location(ByVal location As String) As String
        Dim ds As New DataSet
        strSQL = "SELECT Top 1 Port_or_Bonded as Port FROM  dbo.WareHouse_Loc WHERE LTRIM(RTRIM(X_COORD)) + LTRIM(RTRIM(Y_COORD)) = '" & Trim(location) & "'"
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        Return ds.Tables(0).Rows(0).Item("Port")
    End Function

    Public Function Trucker_Master() As DataSet
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT [COMPANY_NUMBER],[COMPANY_NAME] FROM [CM_System] where TRUCKER='T' and LOCATION_NUMBER=0 order by COMPANY_NAME", 1)
        Return ds
    End Function
    Public Function Project_Master() As DataSet
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT p.Company, p.Company_Loc, c.COMPANY_NAME, p.Project 
                                               FROM dbo.Projects AS p INNER JOIN
                                                    dbo.CM_System AS c ON c.COMPANY_NUMBER = p.Company AND c.LOCATION_NUMBER = p.Company_Loc
                                    ORDER BY c.COMPANY_NAME, p.Project", 1)
        Return ds
    End Function
#End Region

#Region "Booking"
    Public Function DC_BY_BK(ByVal BK As Integer) As DataSet
        Dim ds As New DataSet
        'ds = ws.GetDataset(md.strConnect, "SELECT uid,Booking_Number, isnull(Booking_Seq,991) as Booking_seq , SEQ_NUMBER as seq_Number , UN_NUMBER as UN_NUM, Class_Number, 
        '             DC_DESC as Proper_name,Class_Label,FlashPoint,PAGE_NUMBER,PACKAGING_GROUP,FP_UNIT,SUBRISK, isnull(Pkgs,0) as Pkgs, isnull(PKG_TYPE,'') as  Pkg_Unit, 
        '                 isnull(Weight,0) as Weight, isnull(Weight_Unit,'') as Weight_Unit, Contact, Created_By, Format(Created_On,'MM/dd/yyyy') as Created_On
        '                 FROM Booking_Hazardous 
        '                 WHERE  (Booking_Number = " & BK & ") and (Manual_Flag='N') order by Booking_seq, seq_number", 1)
        strSQL = "Select * From
                (SELECT uid,Booking_Number, isnull(Booking_Seq,991) as Booking_seq , SEQ_NUMBER as seq_Number , UN_NUMBER as UN_NUM, Class_Number, 
                       DC_DESC as Proper_name,Class_Label,FlashPoint,PAGE_NUMBER,PACKAGING_GROUP,FP_UNIT,SUBRISK, isnull(Pkgs,0) as Pkgs, isnull(PKG_TYPE,'') as  Pkg_Unit, 
                                         isnull(Weight,0) as Weight, isnull(Weight_Unit,'') as Weight_Unit, Contact, Created_By, Format(Created_On,'MM/dd/yyyy') as Created_On
                                         FROM Booking_Hazardous 
                                         WHERE  (Booking_Number = " & BK & ") and (Manual_Flag='N')
                Union
                 SELECT  u.uid, u.Booking as Booking_Number, u.BK_Seq as Booking_seq,  h.SEQ_NUMBER, 
                         ISNULL(h.UN_NUMBER, 0) AS UN_Number,
		                 h.Class_Number, 
                         h.DC_DESC as Proper_name,h.Class_Label,h.FlashPoint,h.PAGE_NUMBER,h.PACKAGING_GROUP,h.FP_UNIT,SUBRISK, isnull(h.Pkgs,0) as Pkgs, isnull(h.PKG_TYPE,'') as  Pkg_Unit, 
                                         isnull(h.Weight,0) as Weight, isnull(h.Weight_Unit,'') as Weight_Unit, h.Contact, h.Created_By, Format(h.Created_On,'MM/dd/yyyy') as Created_On
                FROM            dbo.DockReceipt_Pkgs AS u LEFT OUTER JOIN
                                         dbo.DRDC AS h ON u.DR_STEVEDORING = h.DR_STEVEDORING AND u.DR_NUMBER = h.DR_NUMBER AND u.DockReceipt_Pkg_Unit_id = h.Unit_Nro
                WHERE        (u.Booking = " & BK & ") AND (u.DR_STEVEDORING = " & md.Warehouse_D & ") AND (ISNULL(u.HC, '') = 'Y')) as T1
                Where T1.UN_Num <> 0						 
                order by Booking_seq, seq_number"
        ds = ws.GetDataset(strConnect, strSQL, 1)
        Return ds
    End Function
    Public Function DC_BY_BK_Seq(ByVal BK As Integer, ByVal BK_Seq As Integer) As DataSet
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT Booking_Number, isnull(Booking_Seq,991) as Booking_seq , SEQ_NUMBER as seq_Number , UN_NUMBER as UN_NUM, Class_Number, 
       DC_DESC as Proper_name,Class_Label,FlashPoint,PAGE_NUMBER,PACKAGING_GROUP,FP_UNIT,SUBRISK, isnull(Pkgs,0) as Pkgs, isnull(PKG_TYPE,'') as  Pkg_Unit, 
                         Weight, Weight_Unit, Contact, Created_By, Created_On
                         FROM Booking_Hazardous 
                         WHERE  (Booking_Number =" & BK & ") and (Booking_seq = " & BK_Seq & ") and (Manual_Flag='N') order by Booking_seq, seq_number", 1)
        Return ds
    End Function

    Public Function BKs_BY_Job(ByVal Job As String) As DataSet
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "select * from Bookings_Headline where Sailing_nro='" & Trim(Job) & "'", 1)
        Return ds
    End Function
    Public Function BK_Found(ByVal BK As Integer) As Boolean
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "select * from Bookings_Headline where Booking_number=" & BK, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function BK_Count_Lines(ByVal BK As Integer) As Integer
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "SELECT COUNT(*) AS Tot_Lin FROM Booking_Detail Where Booking_Number = " & BK, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                Return ds.Tables(0).Rows(0).Item("Tot_Lin")
            Else
                Return 0
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Public Function BK_Line_Old(ByVal BK As Integer, ByVal Eq_Type As String, ByVal Container As String, ByVal DC_Flag As String) As Integer
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "SELECT seq_number, CONTAINER_TYPE, ISNULL(TIR, 0) AS TIR, ISNULL
                                                 ((SELECT Top 1 Container
                                                     FROM dbo.TIR_Cnt_Transfer
                                                     WHERE (TIR_Cnt_Transfer.Booking_Number = Booking_Detail.Booking_Number and TIR_Cnt_Transfer.Booking_Seq = dbo.Booking_Detail.Seq_Number) Order By DR_Date Desc), '') AS Container, ISNULL
                                                 ((SELECT Top 1 MOVEMENT_TYPE
                                                     FROM dbo.TIR_Cnt_Transfer AS TIRHDR_1
                                                     WHERE (TIRHDR_1.Booking_Number = Booking_Detail.Booking_Number and TIRHDR_1.Booking_Seq = dbo.Booking_Detail.Seq_Number) Order By DR_Date Desc), '') AS Move_Status,
                                                ISNULL(DC_Flag, 'N') AS DC_Flag
                                                  FROM dbo.Booking_Detail Where Booking_Number = " & BK & " Order by seq_Number", 1)
            If ds.Tables(0).Rows.Count > 0 Then
                If Len(Trim(DC_Flag)) = 0 Then
                    DC_Flag = "N"
                End If
                Dim vDC_Flag As String = ""
                Dim Found_Type As Integer = 0
                Dim j As Integer = 0
                For j = 0 To ds.Tables(0).Rows.Count - 1
                    If Trim(ds.Tables(0).Rows(j).Item("Container_Type")) = "LCL" Then
                        Return Trim(ds.Tables(0).Rows(j).Item("seq_Number"))
                        Exit Function
                    End If

                    If Trim(ds.Tables(0).Rows(j).Item("Container_Type")) = Trim(Eq_Type) Then
                        Found_Type = 1
                        If Len(Trim(ds.Tables(0).Rows(j).Item("DC_Flag"))) = 0 Then
                            vDC_Flag = "N"
                        Else
                            vDC_Flag = Trim(ds.Tables(0).Rows(j).Item("DC_Flag"))
                        End If

                        If Trim(vDC_Flag) = Trim(DC_Flag) Then
                            If ds.Tables(0).Rows(j).Item("TIR") = 0 Then
                                Return ds.Tables(0).Rows(j).Item("seq_number")
                                Exit Function
                            Else
                                ' ------- Looking for Container ----------------
                                If Trim(ds.Tables(0).Rows(j).Item("Container")) = Trim(Container) Then
                                    Return ds.Tables(0).Rows(j).Item("seq_number")
                                    Exit Function
                                Else
                                    'Return ds.Tables(0).Rows(j).Item("seq_number")
                                    'Exit Function
                                End If
                            End If
                        End If ' ------- DC Flag
                    End If ' ------- Container Type
                Next
                If Found_Type = 0 Then
                    Return 0
                Else
                    Return ds.Tables(0).Rows(j).Item("seq_number")
                End If
            Else
                Return 1
            End If
            ds = Nothing
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Public Function BK_Line(ByVal BK As Integer, ByVal Eq_Type As String, ByVal Container As String, ByVal Move As String) As Integer
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            Dim ds As New DataSet
            If Mid(Move, 1, 3) = "LOD" Or Mid(Move, 1, 3) = "LOD" Then
                'strSQL = "SELECT Top 1 Booking_seq, Equipment_TYPE FROM dbo.TIR_Cnt_Transfer Where Booking_Number = 10677558 and Container = 'KOSU-621559-7' order by  DR_DATE desc, MOVEMENT_TIME desc"
                strSQL = "SELECT Top 1 Booking_seq, Equipment_TYPE FROM dbo.TIR_Cnt_Transfer Where Booking_Number = " & BK & " and Container = '" & Trim(Container) & "' order by  DR_DATE desc, MOVEMENT_TIME desc"
                ds = ws.GetDataset(strConnect, strSQL, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    Return ds.Tables(0).Rows(0).Item("Booking_seq")
                    Exit Function
                Else
                    Return 0
                    Exit Function
                End If
            Else
                ' ------- Change done on 8/24/2020, looking for Container in TIR to get the seq. From here
                ds = ws.GetDataset(strConnect, "SELECT Top 1 Booking_seq, Equipment_TYPE FROM dbo.TIR_Cnt_Transfer Where Booking_Number = " & BK & " and Container = '" & Trim(Container) & "' order by  DR_DATE desc, MOVEMENT_TIME desc", 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    Return ds.Tables(0).Rows(0).Item("Booking_seq")
                    Exit Function
                Else
                    ' ------- Change done on 8/24/2020 To here
                    ds.Clear()
                    ds = ws.GetDataset(md.strConnect, "SELECT seq_number, CONTAINER_TYPE, ISNULL(TIR, 0) AS TIR, ISNULL
                                                 ((SELECT Top 1 Container
                                                     FROM dbo.TIR_Cnt_Transfer
                                                     WHERE (TIR_Cnt_Transfer.DR_Number = dbo.Booking_Detail.TIR ) Order By DR_Date Desc), '') AS Container, ISNULL
                                                 ((SELECT Top 1 MOVEMENT_TYPE
                                                     FROM dbo.TIR_Cnt_Transfer AS TIRHDR_1
                                                     WHERE (DR_NUMBER = dbo.Booking_Detail.TIR ) Order By DR_Date Desc), '') AS Move_Status
                                                  FROM dbo.Booking_Detail Where Booking_Number = " & BK, 1)

                    If ds.Tables(0).Rows.Count > 0 Then
                        Dim j As Integer = 0
                        For j = 0 To ds.Tables(0).Rows.Count - 1
                            If Trim(ds.Tables(0).Rows(j).Item("Container_Type")) = Trim(Eq_Type) Then
                                If ds.Tables(0).Rows(j).Item("TIR") = 0 Then
                                    Return ds.Tables(0).Rows(j).Item("seq_number")
                                    Exit Function
                                Else
                                    ' ------- Looking for Container ----------------
                                    If Trim(ds.Tables(0).Rows(j).Item("Container")) = Trim(Container) Then
                                        Return ds.Tables(0).Rows(j).Item("seq_number")
                                        Exit Function
                                    End If
                                End If
                            End If
                        Next
                        Return 1
                    Else
                        Return 1
                    End If
                End If

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Public Function BK_Last() As Integer
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "SELECT TOP (1) BOOKING_NUMBER FROM Bookings_Headline Where (BOOKING_NUMBER < 99887700) ORDER BY BOOKING_NUMBER DESC", 1)
            If ds.Tables(0).Rows.Count > 0 Then
                Return ds.Tables(0).Rows(0).Item("Booking_Number")
            Else
                Return 10000000
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Public Function BK_First() As Integer
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "SELECT TOP (1) BOOKING_NUMBER FROM Bookings_Headline ORDER BY BOOKING_NUMBER", 1)
            If ds.Tables(0).Rows.Count > 0 Then
                Return ds.Tables(0).Rows(0).Item("Booking_Number")
            Else
                Return 10000000
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Public Function BK_Audit_BY_BK(ByVal BK As Integer) As DataSet
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT Booking_Number, SEQ_NUM, CLAUSE, Booking_seq, Format(CREATION_DATE,'MM/dd/yyyy') as Creation_Date, CREATION_TIME, CREATED_BY, DESCRIPTION, isnull(Username,'') as Username FROM BKAudit
                         WHERE  (Booking_Number =" & BK & ") and (rtrim(Description) <> 'Open') and (rtrim(Description) <> 'Closed') Order By uid desc", 1)
        Return ds
    End Function
#End Region

#Region "Bill of Loading"

    Public Function BL_Get_Shipper(ByVal BL As String) As Integer
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, "Select Top 1 Shipper_Number From BillOfLoadings Where BL_Number = '" & Trim(BL) & "'", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Shipper_Number")
            ds = Nothing
        Else
            ds = Nothing
            Return 0
        End If
    End Function

    Public Function BL_Paid(ByVal BL As String) As Integer
        Dim ds As New DataSet
        strSQL = "SELECT SUM(ISNULL(AMOUNT_OPEN, 0)) AS Amount_Open FROM dbo.ARHDR WHERE (BL_NUMBER = '" & Trim(BL) & "')"
        ds = ws.GetDataset(strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Amount_Open")
            ds = Nothing
        Else
            ds = Nothing
            Return 0
        End If
    End Function

    Public Function BL_Get_AR(ByVal BL As String) As Integer
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, "SELECT INVOICE_NUMBER, BL_NUMBER FROM dbo.ARHDR WHERE (BL_NUMBER = '" & Trim(BL) & "')", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Invoice_Number")
            ds = Nothing
        Else
            ds = Nothing
            Return 0
        End If
    End Function

    Public Function BL_Get_ARs(ByVal BL As String) As DataSet
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, "SELECT INVOICE_NUMBER, BL_NUMBER FROM dbo.ARHDR WHERE (BL_NUMBER = '" & Trim(BL) & "')", 1)
        Return ds
        ds = Nothing
    End Function

    Public Function BL_Get_Cons(ByVal BL As String) As Integer
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, "Select Top 1 Cons_Number From BillOfLoadings Where BL_Number = '" & Trim(BL) & "'", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Cons_Number")
            ds = Nothing
        Else
            ds = Nothing
            Return 0
        End If
    End Function
    Public Function BL_Get_FWDR(ByVal BL As String) As Integer
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, "Select Top 1 FWDR_Number From BillOfLoadings Where BL_Number = '" & Trim(BL) & "'", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("FWDR_Number")
            ds = Nothing
        Else
            ds = Nothing
            Return 0
        End If
    End Function
    Public Function BL_Get_Notify(ByVal BL As String) As Integer
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, "Select Top 1 Notify_Number From BillOfLoadings Where BL_Number = '" & Trim(BL) & "'", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Notify_Number")
            ds = Nothing
        Else
            ds = Nothing
            Return 0
        End If
    End Function
    Public Function BL_Get_Shipper_Loc(ByVal BL As String) As Integer
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, "Select Top 1 Shipper_Loc From BillOfLoadings Where BL_Number = '" & Trim(BL) & "'", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Shipper_Loc")
            ds = Nothing
        Else
            ds = Nothing
            Return 0
        End If
    End Function
    Public Function BL_Get_Cons_Loc(ByVal BL As String) As Integer
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, "Select Top 1 Cons_Loc From BillOfLoadings Where BL_Number = '" & Trim(BL) & "'", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Cons_Loc")
            ds = Nothing
        Else
            ds = Nothing
            Return 0
        End If
    End Function
    Public Function BL_Get_FWDR_Loc(ByVal BL As String) As Integer
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, "Select Top 1 FWDR_Loc From BillOfLoadings Where BL_Number = '" & Trim(BL) & "'", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("FWDR_Loc")
            ds = Nothing
        Else
            ds = Nothing
            Return 0
        End If
    End Function
    Public Function BL_Get_Notify_Loc(ByVal BL As String) As Integer
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, "Select Top 1 Notify_Loc From BillOfLoadings Where BL_Number = '" & Trim(BL) & "'", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Notify_Loc")
            ds = Nothing
        Else
            ds = Nothing
            Return 0
        End If
    End Function
    Public Function BL_Posted(ByVal BL As String) As Boolean
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, "Select isnull(Posted,'') as Posted From BillOfLoadings Where BL_Number = '" & Trim(BL) & "'", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            If Trim(ds.Tables(0).Rows(0).Item("Posted")) = "P" Then
                ds = Nothing
                Return True
            Else
                ds = Nothing
                Return False
            End If
        Else
            ds = Nothing
            Return False
        End If
    End Function
    Public Function Bill_To_Rules(ByVal BL As String, ByVal PC As String) As String
        If Trim(PC) = "P" Then
            If md.BL_Get_FWDR(BL) <> 999999 Then
                Return "F"
            Else
                Return "S"
            End If
        Else
            If md.BL_Get_Notify(BL) <> 999999 Then
                Return "N"
            Else
                Return "C"
            End If
        End If
    End Function
    Public Function BL_Audit_BY_BL(ByVal BL As String) As DataSet
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT BL, Description, Created_ON, Created_By, Old_value FROM BL_Journal
                         WHERE  (BL ='" & Trim(BL) & "') Order By uid desc", 1)
        Return ds
    End Function
    Public Function BL_Rate_Automatic(ByVal Bill_To As Integer, Bill_Loc As Integer, ByVal BL As String, ByVal vTerm As String) As String
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        ' ------- Change done by Carlos Bajo on 2/14/2019 ------------------------------------
        Dim vContract As String = ""
        Dim ds_Contract As New DataSet
        ds_Contract = ws.GetDataset(md.strConnect, "Select isnull(SERV_CONTRACT,'') as Serv_Contract From BillOfLoadings Where BL_Number = '" & Trim(BL) & "'", 1)
        If ds_Contract.Tables(0).Rows.Count > 0 Then
            If Len(Trim(ds_Contract.Tables(0).Rows(0).Item("Serv_Contract"))) > 0 Then
                If Trim(ds_Contract.Tables(0).Rows(0).Item("Serv_Contract")) <> ".N/A." Then
                    vContract = Trim(ds_Contract.Tables(0).Rows(0).Item("Serv_Contract"))
                End If
            End If
        End If

        Dim Sailing As String = ""
        Dim T_P As Double = 0.00
        Dim T_C As Double = 0.00
        Dim Tot As Double = 0.00
        Dim nCommodty As Integer = 0
        Dim nPortO As Integer = 0
        Dim nPortL As Integer = 0
        Dim nPortD As Integer = 0
        Dim nPortT As Integer = 0
        Dim vEq As String = ""
        Dim strSQL As String = ""
        Dim vHC As String = ""
        Dim nSeq As Integer = 0
        Dim ds_BL_LInes As New DataSet
        ds_BL_LInes.Clear()
        ds_BL_LInes = ws.GetDataset(md.strConnect,
                   "SELECT bk.Sailing_nro, isnull(bl.Point_Origin_Number,1) as PortO_Number,bk.PORT_LOADING as PortL_Number,bk.PORT_DISCHARGE as PortD_Number,bk.PORT_TRANSH as PortT_Number,
                           bl.CREATION_DATE, bl.BL_NUMBER,
                           bd.SEQ_NUMBER,bd.NO_OF_PKGS,
                           isnull(bd.COMMODITY_CODE,0) as Commodity_Code,bd.EQUIPMENT_TYPE,
                           bd.WEIGHT,bd.WEIGHT_UNITS,bd.MEASUREMENTS,bd.MEASURE_UNITS
                           ,bd.DC_Flag
                           ,bd.PKGS_TYPE_CNTR,bd.PKGS_IN_CNTR
                       FROM BillOfLoadings as bl Inner Join
                             Bookings_Headline as bk on bk.Booking_Number = bl.Booking_Number Inner Join
                             BLDTL as bd on bl.BL_number = bd.BL_Number
                      where bl.BL_NUMBER='" & Trim(BL) & "'", 1)
        If ds_BL_LInes.Tables(0).Rows.Count > 0 Then
            Sailing = Trim(ds_BL_LInes.Tables(0).Rows(0).Item("Sailing_nro"))
            nPortO = ds_BL_LInes.Tables(0).Rows(0).Item("PortO_Number")
            nPortL = ds_BL_LInes.Tables(0).Rows(0).Item("PortL_Number")
            nPortD = ds_BL_LInes.Tables(0).Rows(0).Item("PortD_Number")
            nPortT = ds_BL_LInes.Tables(0).Rows(0).Item("PortT_Number")
            Dim ds As New DataSet
            Dim j As Integer = 0
            For j = 0 To ds_BL_LInes.Tables(0).Rows.Count - 1
                nSeq = ds_BL_LInes.Tables(0).Rows(j).Item("Seq_Number")
                nCommodty = ds_BL_LInes.Tables(0).Rows(j).Item("COMMODITY_CODE")
                vEq = ds_BL_LInes.Tables(0).Rows(j).Item("EQUIPMENT_TYPE")
                vHC = ds_BL_LInes.Tables(0).Rows(j).Item("DC_Flag")
                ' ------- Detail
                strSQL = "Select Distinct seq_Number,Charge_number,Description,Equipment_Type,R_Type,max(Flat_Fee) as Flat_Fee,Max(isnull(M_Rate,0)) as M_Rate,max(isnull(W_Rate,0)) as W_Rate,max(Minimum) as Minimum From
                            (Select bl.seq_Number, r.CHARGE_NUMBER, c.DESCRIPTION,r.EQUIPMENT_TYPE,r.Rating_Type,
                                   Case r.RATING_TYPE
	                                 when 'M' then r.Flat_Fee
	                               End As M_Rate,
	                               Case r.RATING_TYPE
	                                 when 'W' then r.Flat_Fee
	                               End As W_Rate,
	                               Case 
	                                 when (r.RATING_TYPE='M' or r.RATING_TYPE='W') then 'W/M'
		                             when r.RATING_TYPE='LS' then 'LS'
                                     when r.Rating_type = 'FLAT' then 'FLAT'
	                               End As R_Type,
	                               Case 
	                                 when (r.RATING_TYPE='M' or r.RATING_TYPE='W') then 1
		                             when r.RATING_TYPE='LS' then Flat_Fee
                                     when r.Rating_type = 'FLAT' then Flat_Fee
	                               End As Flat_Fee
	                               ,r.PERCENTAGE, (isnull(r.FLAT_FEE,0)) As Rate, 
		                           (isnull(r.EXTRA_FLAT_FEE,0)) as Extra_Flat_Fee ,(isnull(r.MINIMUM,0)) as Minimum, isnull(r.maximum,0) as maximum
		                 From Rate_Services As r INNER Join
                                    CARGOS As c On r.CHARGE_NUMBER = c.CHARGE_NUMBER INNER Join
                                    COMDTY As cc On r.COMDTY_ID = cc.ID Inner Join
                                    BLDtl As BL On r.EQUIPMENT_TYPE = bl.EQUIPMENT_TYPE And
                                   (BL.COMMODITY_CODE = r.COMDTY_ID or r.Comdty_ID = 0 or r.Comdty_ID = -1)		 
		               Where
                                    (r.LINE_NUMBER = " & md.Line_default_number & ") And BL.BL_NUMBER ='" & Trim(BL) & "' and bl.Seq_Number = " & nSeq & " and 
                                    (c.Rate_Group = 'F') and
                                    (r.CONTRACT_NUMBER = '" & Trim(vContract) & "') and
                                   (r.COMDTY_ID = " & nCommodty & " OR r.COMDTY_ID = 0 or r.Comdty_ID = -1) and
		                   (r.PORTO_NUMBER = " & nPortO & ") and
		                   (r.PORTL_NUMBER = " & nPortL & ") and
		                   (r.PORTD_NUMBER = " & nPortD & ") and
		                   (r.PORTT_NUMBER = " & nPortT & ") and "
                If Len(Trim(vHC)) = 0 Or Trim(vHC) = "N" Then
                    strSQL = Trim(strSQL) & " r.Charge_Number <> 43 and"
                End If
                strSQL = Trim(strSQL) & " ('" & System.DateTime.Now & "' Between r.Date_From and r.Date_To ) and
		   		           (r.EQUIPMENT_TYPE = '" & Trim(vEq) & "') and r.Flat_Fee <> 0 ) as T1
                          Group By seq_number,CHARGE_NUMBER,Description,EQUIPMENT_TYPE,R_Type
                          Order By seq_Number, CHARGE_NUMBER"
                ds.Clear()
                ds = ws.GetDataset(md.strConnect, strSQL, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim ds_ConvFac As New DataSet
                    Dim vRate_Type As String
                    Dim nAMT As Double = 0.00
                    Dim nValue As Double = 0.00
                    Dim nWeight As Double = 0.00
                    Dim nMeasure As Double = 0.00
                    Dim W_Convert_Factor As Double = 0.00
                    Dim M_Convert_Factor As Double = 0.00
                    Dim Factor_KG_LB As Double = 0.00
                    Dim nRate As Double = 0.00

                    Dim BL_w_Unit As String = ""
                    Dim R_w_Unit As String = ""
                    Dim BL_m_Unit As String = ""
                    Dim R_m_Unit As String = ""
                    T_C = 0
                    T_P = 0
                    Tot = 0
                    nAMT = 0
                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        vRate_Type = ds.Tables(0).Rows(i).Item("R_Type")
                        Select Case Trim(ds.Tables(0).Rows(i).Item("R_Type"))
                            Case "W/M"
                                nValue = 0
                                W_Convert_Factor = 0
                                M_Convert_Factor = 0
                                nWeight = 0
                                nMeasure = 0
                                nRate = 0.00
                                BL_w_Unit = ""
                                R_w_Unit = "KG"
                                BL_m_Unit = ""
                                R_m_Unit = "M3"
                                BL_w_Unit = Trim(ds_BL_LInes.Tables(0).Rows(j).Item("Weight_Units"))
                                BL_m_Unit = Trim(ds_BL_LInes.Tables(0).Rows(j).Item("Measure_UNITS"))
                                nRate = ds.Tables(0).Rows(i).Item("W_Rate")
                                Dim ds_Unit As New DataSet
                                ds_Unit = ws.GetDataset(md.strConnect, "Select MUnit, WUnit From Ports Where Port_Number = " & nPortD, 1)
                                If ds_Unit.Tables(0).Rows.Count > 0 Then
                                    R_m_Unit = ds_Unit.Tables(0).Rows(0).Item("MUnit")
                                    R_w_Unit = ds_Unit.Tables(0).Rows(0).Item("WUnit")
                                End If
                                ' ------- Greater of Weight and Measure
                                ' ------- Weight -------
                                ds_ConvFac.Clear()
                                ds_ConvFac = ws.GetDataset(md.strConnect, "Select Conv_Factor From ConvFac Where Source_Unit = '" & Trim(BL_w_Unit) & "' and Dest_Unit = '" & Trim(R_w_Unit) & "'", 1)
                                If ds_ConvFac.Tables(0).Rows.Count > 0 Then
                                    W_Convert_Factor = ds_ConvFac.Tables(0).Rows(0).Item("Conv_Factor")
                                    If Trim(R_w_Unit) = "KG" Then
                                        Factor_KG_LB = 0.001
                                    Else
                                        Factor_KG_LB = 0.01
                                    End If
                                    nWeight = ((ds_BL_LInes.Tables(0).Rows(j).Item("WEIGHT") * Factor_KG_LB) * W_Convert_Factor) * nRate
                                End If

                                ' ------- Measure -------
                                nRate = 0
                                ds_ConvFac.Clear()
                                ds_ConvFac = ws.GetDataset(md.strConnect, "Select Conv_Factor From ConvFac Where Source_Unit = '" & Trim(BL_m_Unit) & "' and Dest_Unit = '" & Trim(R_m_Unit) & "'", 1)
                                If ds_ConvFac.Tables(0).Rows.Count > 0 Then
                                    M_Convert_Factor = ds_ConvFac.Tables(0).Rows(0).Item("Conv_Factor")
                                    If ds.Tables(0).Rows(i).Item("M_Rate") <> 0 Then
                                        nRate = ds.Tables(0).Rows(i).Item("M_Rate")
                                    End If
                                    nMeasure = (ds_BL_LInes.Tables(0).Rows(j).Item("MEASUREMENTS") * M_Convert_Factor) * nRate
                                End If

                                If nWeight > nMeasure Then
                                    nAMT = nWeight
                                Else
                                    nAMT = nMeasure
                                End If
                                nValue = nAMT
                                If nValue < ds.Tables(0).Rows(i).Item("Minimum") Then
                                    nValue = ds.Tables(0).Rows(i).Item("Minimum")
                                End If

                            Case "M"
                                ' ------- Measure -------------
                                ' ------- Measure -------
                                If Trim(ds_BL_LInes.Tables(0).Rows(j).Item("Measure_UNITS")) = "KG" Then
                                    nAMT = ds_BL_LInes.Tables(0).Rows(j).Item("MEASUREMENTS")
                                Else
                                    ds_ConvFac.Clear()
                                    ds_ConvFac = ws.GetDataset(md.strConnect, "Select Conv_Factor From ConvFac Where Source_Unit = '" & Trim(ds_BL_LInes.Tables(0).Rows(j).Item("Measure_UNITS")) & "' and Dest_Unit = 'M3'", 1)
                                    If ds_ConvFac.Tables(0).Rows.Count > 0 Then
                                        M_Convert_Factor = ds_ConvFac.Tables(0).Rows(0).Item("Conv_Factor")
                                        nAMT = ds_BL_LInes.Tables(0).Rows(j).Item("MEASUREMENTS") * M_Convert_Factor
                                    Else
                                        nAMT = ds_BL_LInes.Tables(0).Rows(j).Item("MEASUREMENTS")
                                    End If
                                End If '
                                nValue = nAMT * ds.Tables(0).Rows(i).Item("M_Rate")
                            Case "LS"
                                nValue = ds.Tables(0).Rows(i).Item("Flat_Fee")
                            Case "FLAT"
                                nValue = ds.Tables(0).Rows(i).Item("Flat_Fee")
                        End Select

                        md.eResp = ws.ExecSQL(md.strConnect, "Insert Into BLRate (Bill_Account, Bill_Loc, Sailing_nro,BL_Number, BL_Line,seq_Number,Charge_Number,PC,Rate,M_Rate,W_Rate,Minimum,Created_By,Creation_Date, BL_Inv, Rating_Type) Values (" &
                                         Bill_To & "," & Bill_Loc & ",'" & Trim(Sailing) & "','" & Trim(BL) & "'," & ds.Tables(0).Rows(i).Item("seq_Number") & "," & ds.Tables(0).Rows(i).Item("seq_Number") & "," & ds.Tables(0).Rows(i).Item("Charge_Number") & ",'" & Mid(vTerm, 1, 1) & "'," & nValue & "," & ds.Tables(0).Rows(i).Item("M_Rate") & "," & ds.Tables(0).Rows(i).Item("W_Rate") & "," & ds.Tables(0).Rows(i).Item("Minimum") & ",'" & Trim(System.Environment.UserName) & "','" & System.DateTime.Now & "','B','" & Trim(vRate_Type) & "')")
                    Next
                End If

                'Exit Sub
            Next
        Else
            MsgBox("Have Not rate, ....")
        End If
        ds_BL_LInes = Nothing
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
#End Region

#Region "Spotting"
    Public Function Trucker_Spot_Reserved(ByVal Trucker As Integer, ByVal dDate As Date, ByVal nTime As String) As Integer
        strSQL = "Select T1.s_Hour, count(*) as Qty_reserved from
                        (SELECT h.TRUCKER_NUMBER, s.booking_number, s.booking_seq, s.seq_number, Format(s.Spot_Date,'MM/dd/yyyy') as Spot_Date, 
                               s.Spot_Time, CONVERT( TIME, s.Spot_Time) as s_Time,
	                           DATEPART(hh,s.Spot_Time) as s_Hour,
	                           s.company_name, s.contact_name, s.address1, s.Remark
                         FROM dbo.Booking_Spot_Inst AS s INNER JOIN
                              dbo.Bookings_Headline AS h ON h.BOOKING_NUMBER = s.booking_number INNER JOIN
                              dbo.CM_System AS n ON h.TRUCKER_NUMBER = n.COMPANY_NUMBER AND h.TRUCKER_LOC = n.LOCATION_NUMBER
                        WHERE (h.Trucker_Number = " & Trucker & ") and s.spot_Time <> 'T.B.A.' and s.Spot_date = '" & dDate.ToShortDateString & "') as T1
                        where T1.s_Hour = '" & nTime & "' Group BY T1.s_Hour ORDER BY T1.s_Hour"
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Qty_reserved")
            ds = Nothing
        Else
            ds = Nothing
            Return 0
        End If
    End Function

    Public Function Trucker_Spot_Allowed(ByVal Trucker As Integer, ByVal nTime As String) As Integer
        strSQL = "SELECT  Trucker, h_From, Qty_Allowed FROM  Spot_Setup Where Trucker = " & Trucker & " and h_From = " & nTime
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Qty_Allowed")
            ds = Nothing
        Else
            ds = Nothing
            Return 0
        End If
    End Function

    Public Function BK_Spotting_BY_BK(ByVal BK As Integer) As DataSet
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT booking_number, booking_seq, seq_number, Line_ID, isNULL(Remark,'') as Remark, 
                                                 isNull(Format(Spot_Date,'MM/dd/yyyy'),'') as Spot_Date, isnull(Spot_Time,'') as Spot_Time,
                                                 isNull(Format(PickUp_Date,'MM/dd/yyyy'),'') as PickUp_Date, isnull(PickUp_Time,'') as PickUp_Time,isnull(Bonder,'') as Bonder, 
                                                 isNull(SpotOn,'') as SpotOn, 
                                                 company_number, location_number, company_name, isnull(contact_name,'') as contact_name,
                                                 isnull(eMail,'') as eMail,
                                                 RTRIM(ISNULL(address1,'')) AS address, 
                                                 RTRIM(ISNULL(City,'')) AS City,
                                                 RTRIM(ISNULL(State,'')) AS State,
                                                 RTRIM(ISNULL(zip,'')) AS zip,
                                                 RTRIM(ISNULL(Phone,'')) AS Phone, 
                                                 isnull(tba,'') as tba,  isnull(created_by,'') as Created_by, creation_date
                                            FROM Booking_Spot_Inst where Booking_number=" & BK & " Order By Booking_number", 1)
        Return ds
    End Function

    Public Function BK_Spotting_BY_BK_seq(ByVal BK As Integer, ByVal BK_Seq As Integer, ByVal Seq_Number As Integer) As DataSet
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT booking_number, booking_seq, seq_number, isNULL(Remark,'') as Remark, 
                                                 isNull(Format(Spot_Date,'MM/dd/yyyy'),'') as Spot_Date, isnull(Spot_Time,'') as Spot_Time, 
                                                 isNull(Format(PickUp_Date,'MM/dd/yyyy'),'') as PickUp_Date, isnull(PickUp_Time,'') as PickUp_Time, isnull(Bonder,'') as Bonder,
                                                 isNull(SpotOn,'') as SpotOn, company_number, location_number, company_name, 
                                                 isnull(contact_name,'') as contact_name,
                                                 isnull(eMail,'') as eMail,
                                                 RTRIM(ISNULL(address1,'')) AS address,
                                                RTRIM(ISNULL(City,'')) AS City,
                                                RTRIM(ISNULL(State,'')) AS State,
                                                RTRIM(ISNULL(zip,'')) AS zip,
                                                RTRIM(ISNULL(Phone,'')) AS Phone, 
                                                isnull(tba,'') as tba, isnull(created_by,'') as Created_by, creation_date
                   FROM Booking_Spot_Inst where Booking_number=" & BK & " and Booking_seq = " & BK_Seq & " and Seq_Number = " & Seq_Number, 1)
        Return ds
    End Function
#End Region

#Region "Clauses"

    Public Function Clauses_TIR(ByVal Yard As Integer, ByVal TIR As Integer) As DataSet
        Dim ds As New DataSet
        Try
            ds = ws.GetDataset(md.strConnect, "Select * from TIR_Comments WHERE Yard=" & Yard & " AND TIR = " & TIR & " Order by uid Desc", 1)
            Return ds
        Catch sqlex As Odbc.OdbcException
            MsgBox(sqlex.Message.ToString)
            ds.Clear()
            Return ds
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
            ds.Clear()
            Return ds
        End Try
    End Function
#End Region

#Region "Store Procedures"

#Region "D/R"
    Public Function DC_BY_DR(ByVal WAREHOUSE As Integer, ByVal DR As Integer) As DataSet
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT DR_STEVEDORING,DR_Number, isnull(DR_Seq,991) as DR_seq,isnull(Unit_Nro,1) as Unit_Nro, SEQ_NUMBER as Line_ID, UN_NUMBER as UN_NUM, Class_Number, 
       DC_DESC as Proper_name,Class_Label,FlashPoint,PAGE_NUMBER,PACKAGING_GROUP,FP_UNIT,SUBRISK, Pkgs, PKG_TYPE as  Pkg_Unit, 
                         Weight, Weight_Unit, Contact, Created_By, Created_On
                         FROM DRDC 
                         WHERE (DR_STEVEDORING = " & WAREHOUSE & ") AND (DR_Number =" & DR & ") and (Manual_Flag='N') Order By isnull(DR_Seq,991),isnull(Unit_Nro,0),SEQ_NUMBER", 1)
        Return ds
    End Function


    ' ------- Change done on 11/13/2022, new function
    Public Function DC_Next_Unit_Seq(ByVal Warehouse As Integer, ByVal DR As Integer, ByVal DR_Seq As Integer, ByVal Unit As Integer) As Integer
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim ds As New DataSet
        strSQL = "SELECT COUNT(*) AS Tot_Lin FROM dbo.DRDC WHERE DR_STEVEDORING = " & Warehouse & " and DR_NUMBER = " & DR & " and DR_Seq = " & DR_Seq & " and UNit_nro = " & Unit
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Tot_Lin") + 1
        Else
            Return 1
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    ' ------- Change done on 11/13/2022, new function
    Public Function DC_Next_Seq(ByVal Warehouse As Integer, ByVal DR As Integer) As Integer
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim ds As New DataSet
        strSQL = "SELECT COUNT(*) AS Tot_Lin FROM dbo.DRDC WHERE DR_STEVEDORING = " & Warehouse & " and DR_NUMBER = " & DR
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Tot_Lin") + 1
        Else
            Return 1
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    ' ------- Change done on 10/17/2022, new function
    Public Function DR_Line_Hazardous(ByVal DR As Integer, ByVal nSeq As Integer) As String
        strSQL = "Select DR_STEVEDORING,DR_Number, isnull(DR_Seq,991) As DR_seq,isnull(Unit_Nro,1) As Unit_Nro, SEQ_NUMBER As Line_ID, UN_NUMBER As UN_NUM
                         From DRDC
                     Where (DR_STEVEDORING = " & md.Warehouse_D & ") And (DR_Number = " & DR & ") and DR_Seq = " & nSeq & " And (Manual_Flag ='N') Order By isnull(DR_Seq,991),isnull(Unit_Nro,0),SEQ_NUMBER"
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return "Y"
        Else
            Return "N"
        End If
        ds = Nothing
    End Function

    ' ------- Change done on 10/19/2022, new function
    Public Function DR_Line_InBond(ByVal DR As Integer, ByVal nSeq As Integer) As String
        strSQL = "Select Top 1 Warehouse,DR_Number, isnull(DR_Seq,991) As DR_seq,isnull(Unit,1) As Unit
                         From DRInBond
                     Where (Warehouse = " & md.Warehouse_D & ") And (DR_Number = " & DR & ") and DR_Seq = " & nSeq
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return "Y"
        Else
            Return "N"
        End If
        ds = Nothing
    End Function
#End Region
    Function Port_Delivery_Terminal_Descriptions(ByVal Port As Integer) As String
        Select Case Port
            Case 100
                ' ------- Port Everglades, USA
                Return "Delivery terminal - " & Chr(13) & Chr(10) & "FCL / HEAVY Self-Propelled Machine - LCL ( Vehicles, Self Propelled units and OOG Static Cargo) - Sun Terminals - 4000 McIntosch Rd. Hollywwod, FL 33316"
            Case Else
                Return " "
        End Select
    End Function

#End Region

#Region "B/L"
    Public Function BL_Notes_x_BL(ByVal BL As String) As DataSet
        Dim ds As New DataSet
        strSQL = "Select isnull(Notes,'') as Notes, isnull(Subject,'') as Subject,isnull(Created_By,'') as Created_By,Format(Created_On,'MM/dd/yyyy') as Created_On, uid from BLNotes where BL_number = '" & Trim(BL) & "' Order By Created_On desc"
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        Return ds
    End Function
#End Region


#Region "BL Accounting Notes"
    Public Function GetBLAccountingNotes(ByVal BL As String) As DataSet
        Dim ds As New DataSet
        strSQL = $"SELECT Note, Created_by, Created_on FROM BL_Accounting_Note where BL_Number = '{BL}' order by Created_ON desc"
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        Return ds
    End Function
#End Region
#Region "View"
    Function View_TIRs(ByVal BK As Integer, ByVal Lin As Integer)
        Dim SQL As String = "SELECT LINE_NUMBER, DR_STEVEDORING, DR_NUMBER, DR_DATE, MOVEMENT_TIME, MOVEMENT_TYPE, 
                      RTRIM(CONT_1) + '-' + RTRIM(CONT_2) + '-' + RTRIM(CONT_3) AS Container,  EQUIPMENT_TYPE,
                      RTRIM(CHAS_1) + '-' + RTRIM(CHAS_2)  + '-' + RTRIM(CHAS_3) AS Chassis,
                      RTRIM(ALT1_EQUIP_1) + '-' + RTRIM(ALT1_EQUIP_2) + '-' + RTRIM(ALT1_EQUIP_3) AS GenSet,
                      BOOKING_NUMBER, BOOKING_SEQ, SEAL_NUMBER, TOT_WEIGHT, DC_FLAG, TEMPERATURE, TOT_PACKAGES, TOT_MEASURE, REM_PACKAGES, REM_WEIGHT, REM_MEASURE, 
                      TRUCKER_NUMBER, TRUCKER_LOC, CARRIER_REF, 
                      FROM_NUMBER, FROM_LOC, FROM_REFS, 
                      TO_NUMBER, TO_LOC, TO_REFS, 
                      WHERE_NUM, WHERE_LOC, 
                      CONSIGNEE_NO, CONSIGNEE_LOC, 
                      NOTIFY_NO, NOTIFY_LOC, Sailing_nro, 
                      PORT_ORIGIN, PORT_LOADING, PORT_DISCH, PORT_TRANSH, 
                     POSTED, CREATION_TIME, CREATION_DATE, CREATED_BY, 
                     CNTDTL_TRAN, WHERE_CODE,
                     SHIPPER_EXTRA, FWDR_EXTRA, CONS_EXTRA, NOTIFY_EXTRA
                FROM  DockReceipts 
                WHERE (DR_STEVEDORING = 1150) AND (TRAN_TYPE = 'T')"
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, Trim(SQL), 1)
        Return ds
    End Function
#End Region

#Region "Imports From GDZ to MSL Commands"
    '1- Import From Clauses to BK_DRDTL_seq
    'Select Case [DR_NUMBER] As Booking_Number,[SEQ_NUM],[DRDTL_SEQ_NUM],[DESCRIPTION] FROM [Clauses] WHERE TRAN_TYPE='B' AND CLAUSE= 'N' AND drDTL_SEQ_NUM > 0
    '2- Booking Audit
    'Select DR_NUMBER As Booking_Number, SEQ_NUM, CLAUSE, DRDTL_SEQ_NUM As Booking_seq, CREATION_DATE, CREATION_TIME, CREATED_BY, DESCRIPTION, DATAFLEX_RECNUM_ONE
    'FROM            Clauses
    'WHERE        (TRAN_TYPE = 'B') AND (DRDTL_SEQ_NUM > 0)
    '3- Import from Clauses to CNTNotes.
    'Select DR_NUMBER As Container_ID, SEQ_NUM, CLAUSE As Note, RTRIM(CAST(format(CREATION_DATE, 'MM/dd/yyyy') AS varchar(20))) + ' ' + CREATION_TIME AS Created_On, CREATED_BY
    'FROM Clauses
    'WHERE (REC_TYPE = 'A') AND (TRAN_TYPE = 'C') AND (DR_STEVEDORING = 999999)

    ' Documents , Rec_Type = 'X' from Clauses

    ' ------- Import to BLRouting_Inst_Notify From BLxTran (SELECT [Sailing_nro],[PORTL_NUMBER],[PORTD_NUMBER],[PORTT_NUMBER],[BL_NUMBER],[SEQ_NUMBER],[TEXT] FROM [BLXTRA] where REC_TYPE = 'O' )
    ' ------- Import to BLComments From BLxTran (SELECT [BL_NUMBER],[REC_TYPE],[SEQ_NUMBER],[TEXT] FROM [BLXTRA] where Rec_Type='B' order by BL_number,seq_number)
    ' ------- Update CM_System Yard, Warehouse, Shipper, Consignee,..., etc.
    ' ------- Update CM_System Set Yard = 'Y' Where isnull(CM_System.Yard,'') = '' and Exists (Select Stevedore_no From Bookings_Headline Where Bookings_Headline.STEVEDORE_NO = CM_System.Company_Number )
    ' ------- Update CM_System Set Shipper = 'Y' Where isnull(CM_System.Shipper,'') = '' and Exists (Select Shipper_number, Shipper_Loc From Bookings_Headline Where Bookings_Headline.Shipper_Number = CM_System.Company_Number and Bookings_Headline.Shipper_Loc = CM_System.Location_Number)
    ' ------- Update CM_System Set Consignee = 'C' Where isnull(CM_System.Consignee,'') = '' and Exists (Select Consignee_no, Consignee_Loc From Bookings_Headline Where Bookings_Headline.Consignee_No = CM_System.Company_Number and Bookings_Headline.Consignee_Loc = CM_System.Location_Number)
    ' ------- Update CM_System Set FWDR = 'F' Where isnull(CM_System.FWDR,'') = '' and Exists (Select FWDR_Number, FWDR_Loc From Bookings_Headline Where Bookings_Headline.FWDR_Number = CM_System.Company_Number and Bookings_Headline.FWDR_Loc = CM_System.Location_Number)
    ' ------- Update CM_System Set Notify = 'N' Where isnull(CM_System.Notify,'') = '' and Exists (Select Notify_No, Notify_Loc From Bookings_Headline Where Bookings_Headline.Notify_No = CM_System.Company_Number and Bookings_Headline.Notify_Loc = CM_System.Location_Number)
    ' ------- Update CM_System Set Trucker = 'T' Where isnull(CM_System.Trucker,'') = '' and Exists (Select Trucker_Number, Trucker_Loc From Bookings_Headline Where Bookings_Headline.Trucker_Number = CM_System.Company_Number and Bookings_Headline.Trucker_Loc = CM_System.Location_Number)
    ' ------- 

    ' ------- Clause
    ' ------- CMS Documents
    ' ------- CMS: Rec_Type = 'X' => Documents,  Tran_Type = 'M', DR_Stevedoring => CMS (Company_Number), Clause => File Name of document
    ' ------- Containers: Rec_Type = 'X' => Documents,  Tran_Type = 'Q', DR_Number => Comtainer_ID , Clause => File Name of document

    Public Function BL_PLace_Of_Receipt_x_BL(ByVal BL As String) As DataSet
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT isnull(TEXT,'') as Place_Receipt  FROM BLXTRA WHERE BL_NUMBER='" & Trim(BL) & "' AND Rec_Type='@'", 1)
        Return ds
    End Function


#Region "Notes"
    Public Function GDZ_Notes_DR_TIR_Yard(ByVal Yard As Integer) As DataSet
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "select * from NOTES where Note_Type <> 'BOOKING' and Pointer1 = " & Yard, 1)
        Return ds
    End Function
    Public Function GDZ_Notes_DR_TIR_Yard_TIR(ByVal Yard As Integer, ByVal TIR As Integer) As DataSet
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "select * from NOTES where Note_Type <> 'BOOKING' and Pointer1 = " & Yard & " and Pointer2 = " & TIR, 1)
        Return ds
    End Function
#End Region
#End Region

    Public Function Contract(ByVal Contract_Number As String) As DataSet
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim ds As New DataSet
        strSQL = "SELECT Top 20 Contract_Number FROM CONTRACT_HDR where Contract_Number like '" & Trim(Replace(Contract_Number, "'", "")) & "%' order by Contract_Number"
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        Return ds
        ds = Nothing
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Public Function Contract_Customer_Name(ByVal Contract_Number As String) As String
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim ds As New DataSet
        strSQL = "SELECT isnull(Customer_Name,'') as Customer_Name FROM CONTRACT_HDR where Contract_Number = '" & Trim(Contract_Number) & "'"
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return Trim(ds.Tables(0).Rows(0).Item("Customer_Name"))
        Else
            Return ""
        End If
        ds = Nothing
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Public Function Contract_Draft_or_ContractSigned(ByVal Contract_Number As String) As String
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim ds As New DataSet
        strSQL = "SELECT CONTRACT_NUMBER, Customer_Number, Customer_Name FROM dbo.Contract_Cust where Contract_Number = '" & Trim(Contract_Number) & "'"
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return "Contract"
        Else
            Return "DRAFT"
        End If
        ds = Nothing
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Public Function Contract_Customer_Linked(ByVal Contract_Number As String) As DataSet
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim ds As New DataSet
        strSQL = "SELECT CONTRACT_NUMBER, Customer_Number, Customer_Name FROM dbo.Contract_Cust WHERE (CONTRACT_NUMBER = '" & Trim(Contract_Number) & "') ORDER BY Customer_Name"
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        Return ds
        ds = Nothing
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Public Function Contract_Start_Date(ByVal Contract_Number As String) As Date
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim ds_Contract_Dates As New DataSet
        ds_Contract_Dates = ws.GetDataset(strConnect, "SELECT CONTRACT_NUMBER, isnull(Amendment_nro,0) as Amendment_nro, Format(START_DATE,'MM/dd/yyyy') as Start_Date FROM  Contract_HDR WHERE (CONTRACT_NUMBER = '" & Trim(Contract_Number) & "')", 1)
        If ds_Contract_Dates.Tables(0).Rows.Count > 0 Then
            Return ds_Contract_Dates.Tables(0).Rows(0).Item("Start_Date")
        Else
            Return System.DateTime.Today.ToShortDateString
        End If
        ds_Contract_Dates = Nothing
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Public Function Contract_End_Date(ByVal Contract_Number As String) As Date
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim ds_Contract_Dates As New DataSet
        ds_Contract_Dates = ws.GetDataset(strConnect, "SELECT CONTRACT_NUMBER, isnull(Amendment_nro,0) as Amendment_nro, Format(End_DATE,'MM/dd/yyyy') as End_Date FROM  Contract_HDR WHERE (CONTRACT_NUMBER = '" & Trim(Contract_Number) & "')", 1)
        If ds_Contract_Dates.Tables(0).Rows.Count > 0 Then
            Return ds_Contract_Dates.Tables(0).Rows(0).Item("End_Date")
        Else
            Return System.DateTime.Today.ToShortDateString
        End If
        ds_Contract_Dates = Nothing
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Public Function Contract_Amendment(ByVal Contract_Number As String) As Integer
        ' ------- Change done on 09/01/2022 
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim nAmendment As Integer = 0
        Dim dsT As New DataSet
        ' ------- Change done on 3/1/2021
        strSQL = "SELECT Top 1 isnull(Amendment_nro, 0) as Amendment FROM dbo.Contract_HDR Where Contract_number = '" & Trim(Contract_Number) & "' Order By isnull(Amendment_nro,0) Desc"
        dsT = ws.GetDataset(strConnect, strSQL, 1)
        If dsT.Tables(0).Rows.Count > 0 Then
            If dsT.Tables(0).Rows(0).Item("Amendment") > 0 Then
                nAmendment = dsT.Tables(0).Rows(0).Item("Amendment")
            Else
                nAmendment = 0
            End If
        Else
            nAmendment = 0
        End If
        dsT = Nothing
        Return nAmendment
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Public Function Contract_Signed(ByVal Contract_Number As String) As String
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim ds As New DataSet
        strSQL = "SELECT  isnull(Signed,'N') as Signed FROM Contract_HDR where Contract_Number = '" & Trim(Contract_Number) & "'"
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Signed")
        Else
            Return "N"
        End If
        ds = Nothing
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Public Function Contract_AmendmentID_From_RateServices(ByVal Adjustment_ID As Integer, ByVal Contract As String) As Integer
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim ds As New DataSet
        strSQL = "SELECT Top 1 ISNULL(Amendment_nro, 0) AS Amendment FROM Rate_Services Where Adjustment_ID = " & Adjustment_ID & " and Contract_Number = ' " & Trim(Contract) & "' order by uid Desc"
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Amendment")
        Else
            Return 0
        End If
        ds = Nothing
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
#Region "Bank"
    Public Function Bank_Account() As DataSet
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT rtrim(ACCOUNT) + ' - ' + Rtrim(DESC_ENG) as Account_To_Show,ACCOUNT,DESC_ENG, DEPARTMENT,NORMAL_BALANCE
    FROM GLACCT where COMPANY_NUMBER=1 and ACCT_GROUP = 'B' and acct_level = 0 and ACTIVE_ACCOUNT = 'Y' order by ACCOUNT", 1)
        Return ds
    End Function
#End Region

#Region "txt_File"
    Function Write_File() As String
        Try
            Dim fPath = "C:\Users\Carlos Bajo\Documents\Abc.txt"
            Dim afile As New IO.StreamWriter(fPath, True)
            Dim j As Integer = 0
            Dim vDate As String
            For j = -60 To 365
                vDate = System.DateTime.Today.AddDays(j)
                'Me.dgv_BL.Columns("ed_Spotting").DefaultCellStyle.
                ' dgv_BL.Columns("drop_column").Items.Add("1")
                afile.WriteLine(vDate)
            Next
            afile.Close()
            Return "Success"
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function
#End Region

    Public Function Insert_Journal(ByVal Sailing_nro As String, ByVal PortL As Integer, ByVal PortD As Integer, ByVal account As String, ByVal Department As String, ByVal tran_date As String, ByVal Document As String, ByVal Je_no As String, ByVal Description As String, ByVal amount As Double, ByVal sign As String, ByVal amount_db As Double, ByVal amount_cr As Double, ByVal Period_Year As Integer, ByVal Period_month As Integer, ByVal Creation_Date As DateTime, ByVal Created_By As String, ByVal Batch As Integer, ByVal Seq As Integer, ByVal BL As String, ByVal Source As String) As String
        ' ------- ACCOUNT JOURNAL -------------------------------------------------------
        Try
            Dim strSQL As String = "Insert into GLDTL (company_number,Sailing_nro,PortL_number,PortD_Number,account, Department, Tran_Date, Document, Je_no, Description, amount, sign, amt_db, amt_cr, Period_Year, Period_month, Creation_date, Created_By,  BATCH_NUMBER, Seq, BL_Number, Posted, Source) values (" & md.GL_Company & ",'" & Sailing_nro & "'," & PortL & "," & PortD & ",'" & Trim(account) & "','" & Trim(Mid(Department, 1, 50)) & "','" & tran_date & "','" & Trim(Document) & "','" & Je_no & "','" & Trim(Replace(Mid(Description, 1, 100), "'", "''")) & "'," & amount & ",'" & sign & "'," & amount_db & "," & amount_cr & "," & Period_Year & "," & Period_month & ",'" & System.DateTime.Now & "','" & Trim(Created_By) & "'," & Batch & "," & Seq & ",'" & BL & "','P','" & Trim(Source) & "')"
            ws.ExecSQL(md.strConnect, "Insert into GLDTL (company_number,Sailing_nro,PortL_number,PortD_Number,account, Department, Tran_Date, Document, Je_no, Description, amount, sign, amt_db, amt_cr, Period_Year, Period_month, Creation_date, Created_By,  BATCH_NUMBER, Seq, BL_Number, Posted, Source) values (" & md.GL_Company & ",'" & Sailing_nro & "'," & PortL & "," & PortD & ",'" & Trim(account) & "','" & Trim(Mid(Department, 1, 50)) & "','" & tran_date & "','" & Trim(Document) & "','" & Mid(Je_no, 1, 8) & "','" & Trim(Replace(Mid(Description, 1, 100), "'", "''")) & "'," & amount & ",'" & sign & "'," & amount_db & "," & amount_cr & "," & Period_Year & "," & Period_month & ",'" & System.DateTime.Now & "','" & Trim(Created_By) & "'," & Batch & "," & Seq & ",'" & BL & "','P','" & Trim(Source) & "')")
            Return "Success"
        Catch ex As Exception
            MsgBox(ex.Message)
            Return ""
        End Try
    End Function

    Public Function SystemDateConvert(ByVal vDate As DateTime) As String
        Dim vYear As String = Trim(vDate.Year.ToString)
        vYear = Mid(vYear, 3, 2)
        Dim vMonth As String = Trim(vDate.Month.ToString)
        If Len(Trim(vMonth)) = 1 Then
            vMonth = "0" & Trim(vMonth)
        End If
        Dim vDay As String = Trim(vDate.Day.ToString)
        If Len(Trim(vDay)) = 1 Then
            vDay = "0" & Trim(vDay)
        End If
        Dim vHH As String = Trim(vDate.Hour.ToString)
        If Len(Trim(vHH)) = 1 Then
            vHH = "0" & Trim(vHH)
        End If
        Dim vmm As String = Trim(vDate.Minute.ToString)
        If Len(Trim(vmm)) = 1 Then
            vmm = "0" & Trim(vmm)
        End If
        Dim vss As String = Trim(vDate.Second.ToString)
        If Len(Trim(vss)) = 1 Then
            vss = "0" & Trim(vss)
        End If
        Return Trim(vYear) & Trim(vMonth) & Trim(vDay) & Trim(vHH) & Trim(vmm) & Trim(vss)
    End Function

    Function RemoveLeadingZeroes(ByVal str)
        Dim tempStr
        tempStr = str
        While Left(tempStr, 1) = "0" And tempStr <> ""
            tempStr = Right(tempStr, Len(tempStr) - 1)
        End While
        RemoveLeadingZeroes = tempStr
    End Function

    Public Function RemoveSpaces(ByVal GeVar As String) As String
        Dim i As Integer
        Dim e As Integer
        Dim NewStr As String = ""
        e = Len(GeVar)
        For i = 1 To e
            If Mid(GeVar, i, 1) <> " " Then
                NewStr = NewStr + Mid(GeVar, i, 1)
            End If
        Next i
        RemoveSpaces = NewStr
        ' MsgBox("alltrim = " & NewStr)
    End Function


#Region "EDI"
    ' ISA01 I01 Authorization Information Qualifier
    Public Authorizatiob_ID As String = "KOSL"

    Public Function ISA(ByVal File_Nro As Integer, ByVal Receiver_ID As String, ByVal vDate As DateTime, ByVal Control_Nro As String, ByVal UsageID As Char) As String
        ' *********** H E A D E R *****************************************************************************************
        ' *********** Interchage Control Header ****************
        Dim lenReceiverID As Integer = 0
        Dim nSpace As Integer = 0
        lenReceiverID = Len(Trim(Receiver_ID))
        nSpace = 15 - lenReceiverID
        Receiver_ID = Trim(Receiver_ID) & Space(nSpace)
        FilePut(File_Nro, "ISA*00*          *00*          *ZZ*" & Trim(md.Authorizatiob_ID) & "           *ZZ*" & Receiver_ID & "*" & Format(vDate, "yyMMdd") & "*" & Format(vDate, "HHmm") & "*U*00401*" & Control_Nro & "*0*" & Trim(UsageID) & "*>" & Chr(13) & Chr(10))
        Return "ISA*00*          *00*          *ZZ*" & Trim(md.Authorizatiob_ID) & "           *ZZ*" & Receiver_ID & "*" & Format(vDate, "yyMMdd") & "*" & Format(vDate, "HHmm") & "*U*00401*" & Control_Nro & "*0*" & Trim(UsageID) & "*>" & Chr(13) & Chr(10)
    End Function
    Public Function GS(ByVal File_Nro As Integer, ByVal Code As String, ByVal Receiver_Code As String, ByVal vDate As DateTime, ByVal Control_Nro As Integer) As String
        ' *********** H E A D E R *****************************************************************************************
        FilePut(File_Nro, "GS*" & Trim(Code) & "*KOSL*" & Trim(Receiver_Code) & "*" & Format(vDate, "yyyyMMdd") & "*" & Format(vDate, "HHmm") & "*" & Control_Nro & "*X*004010" & Chr(13) & Chr(10))                                   ' Write record # 1 to file.
        Return "GS*" & Trim(Code) & "*KOSL*" & Trim(Receiver_Code) & "*" & Format(vDate, "yyyyMMdd") & "*" & Format(vDate, "HHmm") & "*" & Control_Nro & "*X*004010" & Chr(13) & Chr(10)
    End Function
    Public Function ST(ByVal File_Nro As Integer, ByVal TSI_Code As String, ByVal Control_Nro As String) As String
        Try
            FilePut(File_Nro, "ST*" & Trim(TSI_Code) & "*" & Trim(Control_Nro) & Chr(13) & Chr(10))
            Return "ST*" & Trim(TSI_Code) & "*" & Trim(Control_Nro) & Chr(13) & Chr(10)
        Catch ex As Exception
            Return ex.Message
        End Try

    End Function
    Public Function Get_ISA_Control(ByVal EDI As String) As String
        Dim dsEDI_s As New DataSet
        Dim strSQL As String = "Select Control_nro From EDI_Setup Where EDI = '" & Trim(EDI) & "'"
        dsEDI_s = ws.GetDataset(md.strConnect, strSQL, 1)
        Dim nCtr As Integer = 0
        nCtr = dsEDI_s.Tables(0).Rows(0).Item("Control_nro") + 1
        Dim vCtr As String = Str(nCtr)
        Dim n As Integer = Len(Trim(vCtr))
        If (9 - n) < 0 Then
            MsgBox("Control number > 4 dig.")
            Return ""
            Exit Function
        End If
        Dim jj As Integer = 0
        For jj = 1 To 9 - n
            vCtr = "0" & Trim(vCtr)
        Next
        dsEDI_s = Nothing
        strSQL = Nothing
        Return Trim(vCtr)
    End Function
    Public Function Get_GS_Control(ByVal EDI As String) As String
        Dim dsEDI_s As New DataSet
        Dim strSQL As String = "Select Manifest_Seq_nro From EDI_Setup Where EDI = '" & Trim(EDI) & "'"
        dsEDI_s = ws.GetDataset(md.strConnect, strSQL, 1)
        Dim nCtr As Integer = 0
        nCtr = dsEDI_s.Tables(0).Rows(0).Item("Manifest_Seq_nro") + 1
        Dim vCtr As String = Str(nCtr)
        dsEDI_s = Nothing
        strSQL = Nothing
        Return Trim(vCtr)
    End Function
    Public Function Get_ST_Control(ByVal EDI As String) As String
        Dim dsEDI_s As New DataSet
        Dim strSQL As String = "Select File_Nro_Seq From EDI_Setup Where EDI = '" & Trim(EDI) & "'"
        dsEDI_s = ws.GetDataset(md.strConnect, strSQL, 1)
        Dim nCtr As Integer = 0
        nCtr = (dsEDI_s.Tables(0).Rows(0).Item("File_Nro_Seq") * 1000) + 1
        Dim vCtr As String = Str(nCtr)
        dsEDI_s = Nothing
        strSQL = Nothing
        Return Trim(vCtr)
    End Function
    Public Function Get_ST_Control_n(ByVal EDI As String) As Integer
        Dim dsEDI_s As New DataSet
        Dim strSQL As String = "Select File_Nro_Seq From EDI_Setup Where EDI = '" & Trim(EDI) & "'"
        dsEDI_s = ws.GetDataset(md.strConnect, strSQL, 1)
        Dim nCtr As Integer = 0
        nCtr = (dsEDI_s.Tables(0).Rows(0).Item("File_Nro_Seq") * 1000) + 1
        dsEDI_s = Nothing
        strSQL = Nothing
        Return nCtr
    End Function
#End Region

    Public Function Journal(ByVal Created_ON As Date, ByVal Created_Time As String, ByVal Docum_Type As String, ByVal Machine_name As String, ByVal Program_Name As String, ByVal User_name As String, ByVal Desc As String, ByVal Field_Name As String, ByVal From_Value As String, ByVal To_Value As String) As String
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim strSQL As String = ""
        Dim vResp As String = ""
        strSQL = "Insert Into Journal_Changes (Created_On, 
                                               Created_Time, 
                                               Docum_Type, 
                                               Machine_Name, 
                                               Program_Name, 
                                               User_Name, 
                                               Description, 
                                               Field_Name, 
                                               From_Value, 
                                               To_Value
                                                     ) Values ('" &
                                               Format(Created_ON, "MM/dd/yyyy") & "','" &
                                               Trim(Created_Time) & "','" &
                                               Trim(Docum_Type) & "','" &
                                               Trim(Machine_name) & "','" &
                                               Trim(Program_Name) & "','" &
                                               Trim(User_name) & "','" &
                                               Trim(Desc) & "','" &
                                               Trim(Field_Name) & "','" &
                                               Trim(From_Value) & "','" &
                                               Trim(To_Value) & "')"
        vResp = ws.ExecSQL(md.strConnect, strSQL)
        If vResp <> "Success" Then
            MsgBox(Trim(vResp))
        End If
        Return Trim(vResp)
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

#Region "Functions_Process_Data"
    Public Function ExecSQL(ByVal strConnect As String, ByVal strSQL As String) As String
        Try
            Dim cn As New SqlClient.SqlConnection(strConnect)
            cn.Open()
            Dim cmd As SqlClient.SqlCommand = cn.CreateCommand
            cmd.CommandTimeout = 6000
            cmd.CommandText = Trim(strSQL)
            cmd.ExecuteNonQuery()
            cn.Close()
            Return "Success"
        Catch exsql As SqlClient.SqlException
            MsgBox(exsql.Message)
            MsgBox(strSQL)
            Return Trim(exsql.Message)
        Catch ex As Exception
            MsgBox(ex.Message)
            MsgBox(strSQL)
            Return Trim(ex.Message)
        End Try
    End Function

    Public Function DR_SaveImage(ByVal MyImage As Image, ByVal DR As Integer) As String
        Dim ImageBytes(0) As Byte
        Using mStream As New MemoryStream()
            MyImage.Save(mStream, MyImage.RawFormat)
            ImageBytes = mStream.ToArray()
        End Using
        Dim adoConnect = New SqlClient.SqlConnection(strConnection_Local)
        Dim adoCommand = New SqlClient.SqlCommand("UPDATE DRFiles SET [Doc_img]=@MyNewImage WHERE DR_Number =@DR", adoConnect)

        With adoCommand.Parameters.Add("@MyNewImage", SqlDbType.Image)
            .Value = ImageBytes
            .Size = ImageBytes.Length
        End With
        With adoCommand.Parameters.Add("@DR", SqlDbType.BigInt)
            .Value = DR
        End With

        adoConnect.Open()
        adoCommand.ExecuteNonQuery()
        adoConnect.Close()

    End Function

    Public Function ExecSQL_GDZ(ByVal strConnect As String, ByVal strSQL As String) As String
        Try
            Dim cn As New Odbc.OdbcConnection(strConnect)
            cn.Open()
            Dim cmd As Odbc.OdbcCommand = cn.CreateCommand
            cmd.CommandText = Trim(strSQL)
            cmd.ExecuteNonQuery()
            cn.Close()
            Return "Success"
        Catch exsql As Odbc.OdbcException
            MsgBox(exsql.Message)
            MsgBox(strSQL)
            Return Trim(exsql.Message)
        Catch ex As Exception
            MsgBox(ex.Message)
            MsgBox(strSQL)
            Return Trim(ex.Message)
        End Try
    End Function
    Public Function GetDataset(ByVal strConnect As String, ByVal SQLQuery As String, ByVal cmdType As Integer) As DataSet
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim dsGet As New DataSet
        Dim cn As New SqlClient.SqlConnection(strConnect)
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            Dim cmdGet As SqlClient.SqlCommand = cn.CreateCommand
            If cmdType = 1 Then
                cmdGet.CommandType = CommandType.Text
            Else
                cmdGet.CommandType = CommandType.StoredProcedure
            End If
            cmdGet.CommandTimeout = 6000
            cmdGet.CommandText = SQLQuery
            Dim daGet As New SqlClient.SqlDataAdapter(cmdGet)

            dsGet.Clear()
            daGet.Fill(dsGet)
            If dsGet.Tables(0).Rows.Count = 0 Then
                'MsgBox("Your query: " & Trim(SQLQuery) & " is empty")
            End If
            cn.Close()
            Return dsGet
        Catch sqlex As SqlClient.SqlException
            cn.Close()
            MsgBox(sqlex.Message.ToString)
            MsgBox(SQLQuery)
            Return dsGet
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message.ToString)
            MsgBox(SQLQuery)
            Return dsGet
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
    End Function
    Public Function GetData_to_ds(ByVal strConnect As String, ByVal ds As DataSet, ByVal SQLQuery As String) As DataSet
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim cn As New SqlClient.SqlConnection(strConnect)
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            Dim cmdGet As SqlClient.SqlCommand = cn.CreateCommand
            cmdGet.CommandType = CommandType.Text
            cmdGet.CommandTimeout = 6000
            cmdGet.CommandText = SQLQuery
            Dim daGet As New SqlClient.SqlDataAdapter(cmdGet)

            daGet.Fill(ds)
            cn.Close()
            Return ds
        Catch sqlex As SqlClient.SqlException
            cn.Close()
            MsgBox(sqlex.Message.ToString)
            MsgBox(SQLQuery)
            Return ds
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message.ToString)
            MsgBox(SQLQuery)
            Return ds
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
    End Function

    Public Function SP(ByVal strConnect As String, ByVal ST_Name As String) As DataSet
        Dim dsGet As New DataSet
        Dim cn As New SqlClient.SqlConnection(strConnect)
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            Dim cmdGet As SqlClient.SqlCommand = cn.CreateCommand
            cmdGet.CommandType = CommandType.StoredProcedure
            cmdGet.CommandText = Trim(ST_Name)
            Dim daGet As New SqlClient.SqlDataAdapter(cmdGet)
            dsGet.Clear()
            daGet.Fill(dsGet)
            If dsGet.Tables(0).Rows.Count = 0 Then
                'MsgBox("Your query: " & Trim(SQLQuery) & " is empty")
            End If
            cn.Close()
            Return dsGet
        Catch sqlex As SqlClient.SqlException
            cn.Close()
            MsgBox(sqlex.Message.ToString)
            Return dsGet
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message.ToString)
            Return dsGet
        End Try
    End Function
    Public Function GetDataset_SP_1_Parameter(ByVal strConnect As String, ByVal SPSQL As String, ByVal ParamValue As Integer) As DataSet
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim dsGet As New DataSet
        Dim cn As New SqlClient.SqlConnection(strConnect)
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            Dim cmdGet As SqlClient.SqlCommand = cn.CreateCommand
            cmdGet.CommandType = CommandType.StoredProcedure
            cmdGet.Parameters.Add("@BK", SqlDbType.Int)
            cmdGet.CommandText = SPSQL
            cmdGet.Parameters("@BK").Value = ParamValue
            Dim daGet As New SqlClient.SqlDataAdapter(cmdGet)
            dsGet.Clear()
            daGet.Fill(dsGet)
            If dsGet.Tables(0).Rows.Count = 0 Then
                'MsgBox("Your query: " & Trim(SQLQuery) & " is empty")
            End If
            cn.Close()
            Return dsGet
        Catch sqlex As SqlClient.SqlException
            cn.Close()
            MsgBox(sqlex.Message.ToString)
            MsgBox(SPSQL)
            Return dsGet
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message.ToString)
            MsgBox(SPSQL)
            Return dsGet
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
    End Function

    Public Function GetDataset_ODBC(ByVal strConnect As String, ByVal SQLQuery As String, ByVal cmdType As Integer) As DataSet
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim cn As New Odbc.OdbcConnection(strConnect)
        Dim dsGet As New DataSet
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            Dim cmdGet As Odbc.OdbcCommand = cn.CreateCommand
            If cmdType = 1 Then
                cmdGet.CommandType = CommandType.Text
            Else
                cmdGet.CommandType = CommandType.StoredProcedure
            End If
            cmdGet.CommandText = SQLQuery
            Dim daGet As New Odbc.OdbcDataAdapter(cmdGet)

            dsGet.Clear()
            daGet.Fill(dsGet)
            If dsGet.Tables(0).Rows.Count = 0 Then
                'MsgBox("Your query: " & Trim(SQLQuery) & " is empty")
            End If
            cn.Close()
            Return dsGet
        Catch sqlex As Odbc.OdbcException
            cn.Close()
            'MsgBox(sqlex.Message.ToString)
            dsGet.Clear()
            Return dsGet
        Catch ex As Exception
            cn.Close()
            'MsgBox(ex.Message.ToString)
            dsGet.Clear()
            Return dsGet
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
    End Function
    ' ------- 1 Parameter
    Public Function SP_1_Param_int(ByVal strConnect As String, ByVal ST_Name As String, ByVal Param_name As String, ByVal Param As Integer) As DataSet
        Dim dsGet As New DataSet
        Dim cn As New SqlClient.SqlConnection(strConnect)
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            Dim cmdGet As SqlClient.SqlCommand = cn.CreateCommand
            cmdGet.CommandType = CommandType.StoredProcedure
            cmdGet.Parameters.Add(Param_name, SqlDbType.Int)
            cmdGet.CommandText = Trim(ST_Name)
            cmdGet.Parameters(Param_name).Value = Param
            Dim daGet As New SqlClient.SqlDataAdapter(cmdGet)
            dsGet.Clear()
            daGet.Fill(dsGet)
            If dsGet.Tables(0).Rows.Count = 0 Then
                'MsgBox("Your query: " & Trim(SQLQuery) & " is empty")
            End If
            cn.Close()
            Return dsGet
        Catch sqlex As SqlClient.SqlException
            cn.Close()
            MsgBox(sqlex.Message.ToString)
            Return dsGet
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message.ToString)
            Return dsGet
        End Try
    End Function
    Public Function SP_1_Param_string(ByVal strConnect As String, ByVal ST_Name As String, ByVal Param_name As String, ByVal Param As String) As DataSet
        Dim dsGet As New DataSet
        Dim cn As New SqlClient.SqlConnection(strConnect)
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            Dim cmdGet As SqlClient.SqlCommand = cn.CreateCommand
            cmdGet.CommandType = CommandType.StoredProcedure
            cmdGet.Parameters.Add(Param_name, SqlDbType.NVarChar)
            cmdGet.CommandText = Trim(ST_Name)
            cmdGet.Parameters(Param_name).Value = Param
            Dim daGet As New SqlClient.SqlDataAdapter(cmdGet)
            dsGet.Clear()
            daGet.Fill(dsGet)
            If dsGet.Tables(0).Rows.Count = 0 Then
                'MsgBox("Your query: " & Trim(SQLQuery) & " is empty")
            End If
            cn.Close()
            Return dsGet
        Catch sqlex As SqlClient.SqlException
            cn.Close()
            MsgBox(sqlex.Message.ToString)
            Return dsGet
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message.ToString)
            Return dsGet
        End Try
    End Function
    Public Function SP_1_Param_int_with_ds(ByVal ds As DataSet, ByVal strConnect As String, ByVal ST_Name As String, ByVal Param_name As String, ByVal Param As Integer) As DataSet
        Dim cn As New SqlClient.SqlConnection(strConnect)
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            Dim cmdGet As SqlClient.SqlCommand = cn.CreateCommand
            cmdGet.CommandType = CommandType.StoredProcedure
            cmdGet.Parameters.Add(Param_name, SqlDbType.Int)
            cmdGet.CommandText = Trim(ST_Name)
            cmdGet.Parameters(Param_name).Value = Param
            Dim daGet As New SqlClient.SqlDataAdapter(cmdGet)
            daGet.Fill(ds)
            If ds.Tables(0).Rows.Count = 0 Then
                'MsgBox("Your query: " & Trim(SQLQuery) & " is empty")
            End If
            cn.Close()
            Return ds
        Catch sqlex As SqlClient.SqlException
            cn.Close()
            MsgBox(sqlex.Message.ToString)
            Return ds
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message.ToString)
            Return ds
        End Try
    End Function
    ' ------- 2 Parameters
    Public Function SP_2_Param_int(ByVal strConnect As String, ByVal ST_Name As String, ByVal Param1_name As String, ByVal Param2_name As String, ByVal Param1 As Integer, ByVal Param2 As Integer) As DataSet
        Dim dsGet As New DataSet
        Dim cn As New SqlClient.SqlConnection(strConnect)
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            Dim cmdGet As SqlClient.SqlCommand = cn.CreateCommand
            cmdGet.CommandTimeout = 360000
            cmdGet.CommandType = CommandType.StoredProcedure
            cmdGet.Parameters.Add(Param1_name, SqlDbType.Int)
            cmdGet.Parameters.Add(Param2_name, SqlDbType.Int)
            cmdGet.CommandText = Trim(ST_Name)
            cmdGet.Parameters(Param1_name).Value = Param1
            cmdGet.Parameters(Param2_name).Value = Param2
            Dim daGet As New SqlClient.SqlDataAdapter(cmdGet)
            dsGet.Clear()
            daGet.Fill(dsGet)
            If dsGet.Tables(0).Rows.Count = 0 Then
                'MsgBox("Your query: " & Trim(SQLQuery) & " is empty")
            End If
            cn.Close()
            Return dsGet
        Catch sqlex As SqlClient.SqlException
            cn.Close()
            MsgBox(sqlex.Message.ToString)
            Return dsGet
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message.ToString)
            Return dsGet
        End Try
    End Function
    Public Function SP_2_Param_int_with_ds(ByVal ds As DataSet, ByVal strConnect As String, ByVal ST_Name As String, ByVal Param1_name As String, ByVal Param2_name As String, ByVal Param1 As Integer, ByVal Param2 As Integer) As DataSet
        Dim cn As New SqlClient.SqlConnection(strConnect)
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            Dim cmdGet As SqlClient.SqlCommand = cn.CreateCommand
            cmdGet.CommandType = CommandType.StoredProcedure
            cmdGet.Parameters.Add(Param1_name, SqlDbType.Int)
            cmdGet.Parameters.Add(Param2_name, SqlDbType.Int)
            cmdGet.CommandText = Trim(ST_Name)
            cmdGet.Parameters(Param1_name).Value = Param1
            cmdGet.Parameters(Param2_name).Value = Param2
            Dim daGet As New SqlClient.SqlDataAdapter(cmdGet)
            daGet.Fill(ds)
            If ds.Tables(0).Rows.Count = 0 Then
                'MsgBox("Your query: " & Trim(SQLQuery) & " is empty")
            End If
            cn.Close()
            Return ds
        Catch sqlex As SqlClient.SqlException
            cn.Close()
            MsgBox(sqlex.Message.ToString)
            Return ds
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message.ToString)
            Return ds
        End Try
    End Function
    Public Function SP_2_Param_1_int_Param2_string(ByVal strConnect As String, ByVal ST_Name As String, ByVal Param1_name As String, ByVal Param2_name As String, ByVal Param1 As Integer, ByVal Param2 As String) As DataSet
        Dim dsGet As New DataSet
        Dim cn As New SqlClient.SqlConnection(strConnect)
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            Dim cmdGet As SqlClient.SqlCommand = cn.CreateCommand
            cmdGet.CommandType = CommandType.StoredProcedure
            cmdGet.Parameters.Add(Param1_name, SqlDbType.Int)
            cmdGet.Parameters.Add(Param2_name, SqlDbType.NVarChar)
            cmdGet.CommandText = Trim(ST_Name)
            cmdGet.Parameters(Param1_name).Value = Param1
            cmdGet.Parameters(Param2_name).Value = Param2
            Dim daGet As New SqlClient.SqlDataAdapter(cmdGet)
            dsGet.Clear()
            daGet.Fill(dsGet)
            If dsGet.Tables(0).Rows.Count = 0 Then
                'MsgBox("Your query: " & Trim(SQLQuery) & " is empty")
            End If
            cn.Close()
            Return dsGet
        Catch sqlex As SqlClient.SqlException
            cn.Close()
            MsgBox(sqlex.Message.ToString)
            Return dsGet
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message.ToString)
            Return dsGet
        End Try
    End Function
    Public Function SP_2_Param_1_int_Param2_DateTime(ByVal strConnect As String, ByVal ST_Name As String, ByVal Param1_name As String, ByVal Param2_name As String, ByVal Param1 As Integer, ByVal Param2 As DateTime) As DataSet
        Dim dsGet As New DataSet
        Dim cn As New SqlClient.SqlConnection(strConnect)
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            Dim cmdGet As SqlClient.SqlCommand = cn.CreateCommand
            cmdGet.CommandType = CommandType.StoredProcedure
            cmdGet.Parameters.Add(Param1_name, SqlDbType.Int)
            cmdGet.Parameters.Add(Param2_name, SqlDbType.DateTime)
            cmdGet.CommandText = Trim(ST_Name)
            cmdGet.Parameters(Param1_name).Value = Param1
            cmdGet.Parameters(Param2_name).Value = Param2
            Dim daGet As New SqlClient.SqlDataAdapter(cmdGet)
            dsGet.Clear()
            daGet.Fill(dsGet)
            If dsGet.Tables(0).Rows.Count = 0 Then
                'MsgBox("Your query: " & Trim(SQLQuery) & " is empty")
            End If
            cn.Close()
            Return dsGet
        Catch sqlex As SqlClient.SqlException
            cn.Close()
            MsgBox(sqlex.Message.ToString)
            Return dsGet
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message.ToString)
            Return dsGet
        End Try
    End Function
    Public Function SP_2_Param_1_DateTime_Param2_Int(ByVal strConnect As String, ByVal ST_Name As String, ByVal Param1_name As String, ByVal Param2_name As String, ByVal Param1 As DateTime, ByVal Param2 As Integer) As DataSet
        Dim dsGet As New DataSet
        Dim cn As New SqlClient.SqlConnection(strConnect)
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            Dim cmdGet As SqlClient.SqlCommand = cn.CreateCommand
            cmdGet.CommandType = CommandType.StoredProcedure
            cmdGet.Parameters.Add(Param1_name, SqlDbType.Int)
            cmdGet.Parameters.Add(Param2_name, SqlDbType.DateTime)
            cmdGet.CommandText = Trim(ST_Name)
            cmdGet.Parameters(Param1_name).Value = Param1
            cmdGet.Parameters(Param2_name).Value = Param2
            Dim daGet As New SqlClient.SqlDataAdapter(cmdGet)
            dsGet.Clear()
            daGet.Fill(dsGet)
            If dsGet.Tables(0).Rows.Count = 0 Then
                'MsgBox("Your query: " & Trim(SQLQuery) & " is empty")
            End If
            cn.Close()
            Return dsGet
        Catch sqlex As SqlClient.SqlException
            cn.Close()
            MsgBox(sqlex.Message.ToString)
            Return dsGet
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message.ToString)
            Return dsGet
        End Try
    End Function
    Public Function SP_2_Param_1_int_Param2_Date(ByVal strConnect As String, ByVal ST_Name As String, ByVal Param1_name As String, ByVal Param2_name As String, ByVal Param1 As Integer, ByVal Param2 As Date) As DataSet
        Dim dsGet As New DataSet
        Dim cn As New SqlClient.SqlConnection(strConnect)
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            Dim cmdGet As SqlClient.SqlCommand = cn.CreateCommand
            cmdGet.CommandType = CommandType.StoredProcedure
            cmdGet.Parameters.Add(Param1_name, SqlDbType.Int)
            cmdGet.Parameters.Add(Param2_name, SqlDbType.Date)
            cmdGet.CommandText = Trim(ST_Name)
            cmdGet.Parameters(Param1_name).Value = Param1
            cmdGet.Parameters(Param2_name).Value = Param2
            Dim daGet As New SqlClient.SqlDataAdapter(cmdGet)
            dsGet.Clear()
            daGet.Fill(dsGet)
            If dsGet.Tables(0).Rows.Count = 0 Then
                'MsgBox("Your query: " & Trim(SQLQuery) & " is empty")
            End If
            cn.Close()
            Return dsGet
        Catch sqlex As SqlClient.SqlException
            cn.Close()
            MsgBox(sqlex.Message.ToString)
            Return dsGet
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message.ToString)
            Return dsGet
        End Try
    End Function
    Public Function SP_2_Param_1_string_Param2_int(ByVal strConnect As String, ByVal ST_Name As String, ByVal Param1_name As String, ByVal Param2_name As String, ByVal Param1 As String, ByVal Param2 As Integer) As DataSet
        Dim dsGet As New DataSet
        Dim cn As New SqlClient.SqlConnection(strConnect)
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            Dim cmdGet As SqlClient.SqlCommand = cn.CreateCommand
            cmdGet.CommandType = CommandType.StoredProcedure
            cmdGet.Parameters.Add(Param1_name, SqlDbType.NVarChar)
            cmdGet.Parameters.Add(Param2_name, SqlDbType.Int)
            cmdGet.CommandText = Trim(ST_Name)
            cmdGet.Parameters(Param1_name).Value = Param1
            cmdGet.Parameters(Param2_name).Value = Param2
            Dim daGet As New SqlClient.SqlDataAdapter(cmdGet)
            dsGet.Clear()
            daGet.Fill(dsGet)
            If dsGet.Tables(0).Rows.Count = 0 Then
                'MsgBox("Your query: " & Trim(SQLQuery) & " is empty")
            End If
            cn.Close()
            Return dsGet
        Catch sqlex As SqlClient.SqlException
            cn.Close()
            MsgBox(sqlex.Message.ToString)
            Return dsGet
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message.ToString)
            Return dsGet
        End Try
    End Function
    ' ------- 3 Parameters
    Public Function SP_3_Param_2_int_1_Date(ByVal strConnect As String, ByVal ST_Name As String, ByVal Param1_name As String, ByVal Param2_name As String, ByVal Param3_name As String, ByVal Param1 As Integer, ByVal Param2 As Integer, ByVal Param3 As Date) As DataSet
        Dim dsGet As New DataSet
        Dim cn As New SqlClient.SqlConnection(strConnect)
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            Dim cmdGet As SqlClient.SqlCommand = cn.CreateCommand
            cmdGet.CommandType = CommandType.StoredProcedure
            cmdGet.Parameters.Add(Param1_name, SqlDbType.Int)
            cmdGet.Parameters.Add(Param2_name, SqlDbType.Int)
            cmdGet.Parameters.Add(Param3_name, SqlDbType.DateTime)
            cmdGet.CommandText = Trim(ST_Name)
            cmdGet.Parameters(Param1_name).Value = Param1
            cmdGet.Parameters(Param2_name).Value = Param2
            cmdGet.Parameters(Param3_name).Value = Param3
            Dim daGet As New SqlClient.SqlDataAdapter(cmdGet)
            dsGet.Clear()
            daGet.Fill(dsGet)
            If dsGet.Tables(0).Rows.Count = 0 Then
                'MsgBox("Your query: " & Trim(SQLQuery) & " is empty")
            End If
            cn.Close()
            Return dsGet
        Catch sqlex As SqlClient.SqlException
            cn.Close()
            MsgBox(sqlex.Message.ToString)
            Return dsGet
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message.ToString)
            Return dsGet
        End Try
    End Function
    Public Function SP_3_Param_3_int(ByVal strConnect As String, ByVal ST_Name As String, ByVal Param1_name As String, ByVal Param2_name As String, ByVal Param3_name As String, ByVal Param1 As Integer, ByVal Param2 As Integer, ByVal Param3 As Integer) As DataSet
        Dim dsGet As New DataSet
        Dim cn As New SqlClient.SqlConnection(strConnect)
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            Dim cmdGet As SqlClient.SqlCommand = cn.CreateCommand
            cmdGet.CommandType = CommandType.StoredProcedure
            cmdGet.Parameters.Add(Param1_name, SqlDbType.Int)
            cmdGet.Parameters.Add(Param2_name, SqlDbType.Int)
            cmdGet.Parameters.Add(Param3_name, SqlDbType.Int)
            cmdGet.CommandText = Trim(ST_Name)
            cmdGet.Parameters(Param1_name).Value = Param1
            cmdGet.Parameters(Param2_name).Value = Param2
            cmdGet.Parameters(Param3_name).Value = Param3
            Dim daGet As New SqlClient.SqlDataAdapter(cmdGet)
            dsGet.Clear()
            daGet.Fill(dsGet)
            If dsGet.Tables(0).Rows.Count = 0 Then
                'MsgBox("Your query: " & Trim(SQLQuery) & " is empty")
            End If
            cn.Close()
            Return dsGet
        Catch sqlex As SqlClient.SqlException
            cn.Close()
            MsgBox(sqlex.Message.ToString)
            Return dsGet
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message.ToString)
            Return dsGet
        End Try
    End Function
    Public Function SP_3_Param_int_2_Date(ByVal strConnect As String, ByVal ST_Name As String, ByVal Param1_name As String, ByVal Param2_name As String, ByVal Param3_name As String, ByVal Param1 As Integer, ByVal Param2 As Date, ByVal Param3 As Date) As DataSet
        Dim dsGet As New DataSet
        Dim cn As New SqlClient.SqlConnection(strConnect)
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            Dim cmdGet As SqlClient.SqlCommand = cn.CreateCommand
            cmdGet.CommandType = CommandType.StoredProcedure
            cmdGet.Parameters.Add(Param1_name, SqlDbType.Int)
            cmdGet.Parameters.Add(Param2_name, SqlDbType.Date)
            cmdGet.Parameters.Add(Param3_name, SqlDbType.Date)
            cmdGet.CommandText = Trim(ST_Name)
            cmdGet.Parameters(Param1_name).Value = Param1
            cmdGet.Parameters(Param2_name).Value = Param2
            cmdGet.Parameters(Param2_name).Value = Param3
            Dim daGet As New SqlClient.SqlDataAdapter(cmdGet)
            dsGet.Clear()
            daGet.Fill(dsGet)
            If dsGet.Tables(0).Rows.Count = 0 Then
                'MsgBox("Your query: " & Trim(SQLQuery) & " is empty")
            End If
            cn.Close()
            Return dsGet
        Catch sqlex As SqlClient.SqlException
            cn.Close()
            MsgBox(sqlex.Message.ToString)
            Return dsGet
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message.ToString)
            Return dsGet
        End Try
    End Function

    ' ------- 4 Parameters
    Public Function SP_4_Param_2_int_2_string(ByVal strConnect As String, ByVal ST_Name As String, ByVal Param1_name As String, ByVal Param2_name As String, ByVal Param3_name As String, ByVal Param4_name As String, ByVal Param1 As Integer, ByVal Param2 As Integer, ByVal Param3 As String, ByVal Param4 As String) As DataSet
        Dim dsGet As New DataSet
        Dim cn As New SqlClient.SqlConnection(strConnect)
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            Dim cmdGet As SqlClient.SqlCommand = cn.CreateCommand
            cmdGet.CommandType = CommandType.StoredProcedure
            cmdGet.Parameters.Add(Param1_name, SqlDbType.Int)
            cmdGet.Parameters.Add(Param2_name, SqlDbType.Int)
            cmdGet.Parameters.Add(Param3_name, SqlDbType.NVarChar)
            cmdGet.Parameters.Add(Param4_name, SqlDbType.NVarChar)
            cmdGet.CommandText = Trim(ST_Name)
            cmdGet.Parameters(Param1_name).Value = Param1
            cmdGet.Parameters(Param2_name).Value = Param2
            cmdGet.Parameters(Param3_name).Value = Param3
            cmdGet.Parameters(Param4_name).Value = Param4
            Dim daGet As New SqlClient.SqlDataAdapter(cmdGet)
            dsGet.Clear()
            daGet.Fill(dsGet)
            If dsGet.Tables(0).Rows.Count = 0 Then
                'MsgBox("Your query: " & Trim(SQLQuery) & " is empty")
            End If
            cn.Close()
            Return dsGet
        Catch sqlex As SqlClient.SqlException
            cn.Close()
            MsgBox(sqlex.Message.ToString)
            Return dsGet
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message.ToString)
            Return dsGet
        End Try
    End Function
    Public Function SP_4_Param_2_int_2_Date(ByVal strConnect As String, ByVal ST_Name As String, ByVal Param1_name As String, ByVal Param2_name As String, ByVal Param3_name As String, ByVal Param4_name As String, ByVal Param1 As Integer, ByVal Param2 As Integer, ByVal Param3 As Date, ByVal Param4 As Date) As DataSet
        Dim dsGet As New DataSet
        Dim cn As New SqlClient.SqlConnection(strConnect)
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            Dim cmdGet As SqlClient.SqlCommand = cn.CreateCommand
            cmdGet.CommandType = CommandType.StoredProcedure
            cmdGet.Parameters.Add(Param1_name, SqlDbType.Int)
            cmdGet.Parameters.Add(Param2_name, SqlDbType.Int)
            cmdGet.Parameters.Add(Param3_name, SqlDbType.Date)
            cmdGet.Parameters.Add(Param4_name, SqlDbType.Date)
            cmdGet.CommandText = Trim(ST_Name)
            cmdGet.Parameters(Param1_name).Value = Param1
            cmdGet.Parameters(Param2_name).Value = Param2
            cmdGet.Parameters(Param3_name).Value = Param3
            cmdGet.Parameters(Param4_name).Value = Param4
            Dim daGet As New SqlClient.SqlDataAdapter(cmdGet)
            dsGet.Clear()
            daGet.Fill(dsGet)
            If dsGet.Tables(0).Rows.Count = 0 Then
                'MsgBox("Your query: " & Trim(SQLQuery) & " is empty")
            End If
            cn.Close()
            Return dsGet
        Catch sqlex As SqlClient.SqlException
            cn.Close()
            MsgBox(sqlex.Message.ToString)
            Return dsGet
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message.ToString)
            Return dsGet
        End Try
    End Function
    Public Function SP_4_Param_int(ByVal strConnect As String, ByVal ST_Name As String, ByVal Param1_name As String, ByVal Param2_name As String, ByVal Param3_name As String, ByVal Param4_name As String, ByVal Param1 As Integer, ByVal Param2 As Integer, ByVal Param3 As Integer, ByVal Param4 As Integer) As DataSet
        Dim dsGet As New DataSet
        Dim cn As New SqlClient.SqlConnection(strConnect)
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            Dim cmdGet As SqlClient.SqlCommand = cn.CreateCommand
            cmdGet.CommandType = CommandType.StoredProcedure
            cmdGet.Parameters.Add(Param1_name, SqlDbType.Int)
            cmdGet.Parameters.Add(Param2_name, SqlDbType.Int)
            cmdGet.Parameters.Add(Param3_name, SqlDbType.Int)
            cmdGet.Parameters.Add(Param4_name, SqlDbType.Int)
            cmdGet.CommandText = Trim(ST_Name)
            cmdGet.Parameters(Param1_name).Value = Param1
            cmdGet.Parameters(Param2_name).Value = Param2
            cmdGet.Parameters(Param3_name).Value = Param3
            cmdGet.Parameters(Param4_name).Value = Param4
            Dim daGet As New SqlClient.SqlDataAdapter(cmdGet)
            dsGet.Clear()
            daGet.Fill(dsGet)
            If dsGet.Tables(0).Rows.Count = 0 Then
                'MsgBox("Your query: " & Trim(SQLQuery) & " is empty")
            End If
            cn.Close()
            Return dsGet
        Catch sqlex As SqlClient.SqlException
            cn.Close()
            MsgBox(sqlex.Message.ToString)
            Return dsGet
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message.ToString)
            Return dsGet
        End Try
    End Function
    Public Function SP_5_Param_3_int_2_Date(ByVal strConnect As String, ByVal ST_Name As String, ByVal Param1_name As String, ByVal Param2_name As String, ByVal Param3_name As String, ByVal Param4_name As String, ByVal Param5_name As String, ByVal Param1 As Integer, ByVal Param2 As Integer, ByVal Param3 As Integer, ByVal Param4 As Date, ByVal Param5 As Date) As DataSet
        Dim dsGet As New DataSet
        Dim cn As New SqlClient.SqlConnection(strConnect)
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            Dim cmdGet As SqlClient.SqlCommand = cn.CreateCommand
            cmdGet.CommandType = CommandType.StoredProcedure
            cmdGet.Parameters.Add(Param1_name, SqlDbType.Int)
            cmdGet.Parameters.Add(Param2_name, SqlDbType.Int)
            cmdGet.Parameters.Add(Param3_name, SqlDbType.Int)
            cmdGet.Parameters.Add(Param4_name, SqlDbType.Date)
            cmdGet.Parameters.Add(Param5_name, SqlDbType.Date)
            cmdGet.CommandText = Trim(ST_Name)
            cmdGet.Parameters(Param1_name).Value = Param1
            cmdGet.Parameters(Param2_name).Value = Param2
            cmdGet.Parameters(Param3_name).Value = Param3
            cmdGet.Parameters(Param4_name).Value = Param4
            cmdGet.Parameters(Param5_name).Value = Param5
            Dim daGet As New SqlClient.SqlDataAdapter(cmdGet)
            dsGet.Clear()
            daGet.Fill(dsGet)
            If dsGet.Tables(0).Rows.Count = 0 Then
                'MsgBox("Your query: " & Trim(SQLQuery) & " is empty")
            End If
            cn.Close()
            Return dsGet
        Catch sqlex As SqlClient.SqlException
            cn.Close()
            MsgBox(sqlex.Message.ToString)
            Return dsGet
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message.ToString)
            Return dsGet
        End Try
    End Function
    ' ------- Parameters 6
    Public Function SP_6_Param_3_int_2_Date_1_int(ByVal strConnect As String, ByVal ST_Name As String, ByVal Param1_name As String, ByVal Param2_name As String, ByVal Param3_name As String, ByVal Param4_name As String, ByVal Param5_name As String, ByVal Param6_name As String, ByVal Param1 As Integer, ByVal Param2 As Integer, ByVal Param3 As Integer, ByVal Param4 As Date, ByVal Param5 As Date, ByVal Param6 As Integer) As DataSet
        Dim dsGet As New DataSet
        Dim cn As New SqlClient.SqlConnection(strConnect)
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            Dim cmdGet As SqlClient.SqlCommand = cn.CreateCommand
            cmdGet.CommandType = CommandType.StoredProcedure
            cmdGet.Parameters.Add(Param1_name, SqlDbType.Int)
            cmdGet.Parameters.Add(Param2_name, SqlDbType.Int)
            cmdGet.Parameters.Add(Param3_name, SqlDbType.Int)
            cmdGet.Parameters.Add(Param4_name, SqlDbType.Date)
            cmdGet.Parameters.Add(Param5_name, SqlDbType.Date)
            cmdGet.Parameters.Add(Param6_name, SqlDbType.Int)
            cmdGet.CommandText = Trim(ST_Name)
            cmdGet.Parameters(Param1_name).Value = Param1
            cmdGet.Parameters(Param2_name).Value = Param2
            cmdGet.Parameters(Param3_name).Value = Param3
            cmdGet.Parameters(Param4_name).Value = Param4
            cmdGet.Parameters(Param5_name).Value = Param5
            cmdGet.Parameters(Param6_name).Value = Param6
            Dim daGet As New SqlClient.SqlDataAdapter(cmdGet)
            dsGet.Clear()
            daGet.Fill(dsGet)
            If dsGet.Tables(0).Rows.Count = 0 Then
                'MsgBox("Your query: " & Trim(SQLQuery) & " is empty")
            End If
            cn.Close()
            Return dsGet
        Catch sqlex As SqlClient.SqlException
            cn.Close()
            MsgBox(sqlex.Message.ToString)
            Return dsGet
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message.ToString)
            Return dsGet
        End Try
    End Function


    Public Function Check_by_Bank_ChkNro(ByVal strConnect As String, ByVal ST_Name As String, ByVal Param1_name As String, ByVal Param2_name As String, ByVal Param3_name As String, ByVal Param4_name As String, ByVal Param5_name As String, ByVal Param6_name As String, ByVal Param7_name As String, ByVal Param8_name As String, ByVal Param1 As Integer, ByVal Param2 As Integer, ByVal Param3 As Integer, ByVal Param4 As Integer, ByVal Param5 As Integer, ByVal Param6 As Integer, ByVal Param7 As Integer, ByVal Param8 As Double) As DataSet
        Dim dsGet As New DataSet
        Dim cn As New SqlClient.SqlConnection(strConnect)
        Try
            If cn.State = ConnectionState.Closed Then cn.Open()
            Dim cmdGet As SqlClient.SqlCommand = cn.CreateCommand
            cmdGet.CommandType = CommandType.StoredProcedure
            cmdGet.Parameters.Add(Param1_name, SqlDbType.Int)
            cmdGet.Parameters.Add(Param2_name, SqlDbType.Int)
            cmdGet.Parameters.Add(Param3_name, SqlDbType.Int)
            cmdGet.Parameters.Add(Param4_name, SqlDbType.Int)
            cmdGet.Parameters.Add(Param5_name, SqlDbType.Int)
            cmdGet.Parameters.Add(Param6_name, SqlDbType.Int)
            cmdGet.Parameters.Add(Param7_name, SqlDbType.Int)
            cmdGet.Parameters.Add(Param8_name, SqlDbType.Float)
            cmdGet.CommandText = Trim(ST_Name)
            cmdGet.Parameters(Param1_name).Value = Param1
            cmdGet.Parameters(Param2_name).Value = Param2
            cmdGet.Parameters(Param3_name).Value = Param3
            cmdGet.Parameters(Param4_name).Value = Param4
            cmdGet.Parameters(Param5_name).Value = Param5
            cmdGet.Parameters(Param6_name).Value = Param6
            cmdGet.Parameters(Param7_name).Value = Param7
            cmdGet.Parameters(Param8_name).Value = Param8
            Dim daGet As New SqlClient.SqlDataAdapter(cmdGet)
            dsGet.Clear()
            daGet.Fill(dsGet)
            If dsGet.Tables(0).Rows.Count = 0 Then
                'MsgBox("Your query: " & Trim(SQLQuery) & " is empty")
            End If
            cn.Close()
            Return dsGet
        Catch sqlex As SqlClient.SqlException
            cn.Close()
            MsgBox(sqlex.Message.ToString)
            Return dsGet
        Catch ex As Exception
            cn.Close()
            MsgBox(ex.Message.ToString)
            Return dsGet
        End Try
    End Function
#End Region

    Public Function FormatStrLine(ByVal v1 As String) As String
        'Dim v1 As String = DsBKHDR_x_BK_R.Tables(0).Rows(0).Item("Shipper_Desc").ToString
        Dim parts As String() = v1.Split(ControlChars.CrLf.ToCharArray)
        v1 = ""
        For Each part As String In parts
            v1 = v1 & part & vbCrLf
        Next
        Return Replace(v1, vbCrLf & vbCrLf, vbCrLf)
    End Function

    Public Function Clear_CRLF(ByVal Desc As String) As String
        If Len(Trim(Desc)) > 0 Then
            Dim v1 As String = ""
            v1 = Trim(Replace(Desc, "'", "''").Replace(vbCr & vbCrLf, ""))
            Return v1
        Else
            Return ""
        End If
    End Function

    Public Function STR_Clear_vbCR(ByVal Desc As String) As String
        If Len(Trim(Desc)) > 0 Then
            Dim v1 As String = ""
            v1 = Trim(Replace(Desc, "'", "''").Replace(vbCr & vbCrLf, vbCrLf))
            Return v1
        Else
            Return ""
        End If
    End Function

#Region "Containers"
    Public Function Container_Notes(ByVal Cont1 As String, ByVal Cont2 As Integer, ByVal Cont3 As String) As String
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "Select Container_ID From Equipment_Master Where Container_no_1 = '" & Trim(Cont1) & "' and Container_no_2 = " & Cont2 & " and Container_no_3 = '" & Trim(Cont3) & "'", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0).Item("Container_Id") > 0 Then
                Dim Cont_ID As Integer = ds.Tables(0).Rows(0).Item("Container_Id")
                ds.Clear()
                ds = ws.GetDataset(md.strConnect, " Select Note From CNTNotes Where Container_Id = " & Cont_ID, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    Return Trim(ds.Tables(0).Rows(0).Item("Note"))
                Else
                    Return ""
                End If
            End If
        End If
    End Function

    Public Function Container_Get_Booking(ByVal Container As String) As Integer
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        strSQL = "SELECT BOOKING_NUMBER FROM  dbo.Equipment_Master WHERE (Container = '" & Trim(Container) & "')"
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Booking_Number")
        Else
            Return 0
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    ' ------- Change done on 10/11/2022, new function to get all BLs for a container
    Public Function Container_Get_BLs(ByVal Container As String) As DataSet
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        strSQL = "SELECT Distinct bl.BL_Number, t.BOOKING_NUMBER
                    FROM dbo.TIR_Cnt_Transfer AS t INNER JOIN
                         dbo.Booking_Detail AS bd ON bd.BOOKING_NUMBER = t.BOOKING_NUMBER AND bd.seq_number = t.BOOKING_SEQ Inner Join
						 dbo.BLDTL AS bl ON bl.Booking_Number = bd.BOOKING_NUMBER AND bl.BK_Seq = bd.seq_number
                    WHERE (t.Container = '" & Trim(Container) & "') AND (t.BOOKING_NUMBER <> 0) AND (ISNULL(bd.BL_Number, N'') <> '')
                    ORDER BY bl.BL_Number DESC"

        ' ------- Change done on 10/13/2022, new function to get all BLs for a container
        strSQL = "SELECT Distinct bl.BL_Number, t.BOOKING_NUMBER,  isnull(b.On_Hold,'') as Hold_Status, bk.Port_loading as PortL, bk.Port_Discharge as PortD,
                       bk.Sailing_nro,
	                   isnull((Select Top 1 Vessel_Name From Sailing_Master Where bk.Sailing_nro = Sailing_Master.Sailing_nro and (bk.Port_Loading = Sailing_Master.port_loading or bk.Port_Transshipment = Sailing_Master.port_Loading)),'') as Vessel,
                       isnull((Select Top 1 Voyage_nro From Sailing_Master Where bk.Sailing_nro = Sailing_Master.Sailing_nro and (bk.Port_Loading = Sailing_Master.port_loading or bk.Port_Transshipment = Sailing_Master.port_Loading)),'') as Voyage,
	                   isnull((Select Top 1 Format(Est_Departure,'MM/dd/yyyy') as Est_Departure From Sailing_Master Where bk.Sailing_nro = Sailing_Master.Sailing_nro and (bk.Port_Loading = Sailing_Master.port_loading or bk.Port_Transshipment = Sailing_Master.port_Loading)),'') as Departure,
	                   sh.Company_name as Shipper, cg.company_name as Consignee, fw.Company_name as FWDR, nf.Company_name as Notify
                  FROM dbo.TIR_Cnt_Transfer AS t INNER JOIN
                     dbo.Booking_Detail AS bd ON bd.BOOKING_NUMBER = t.BOOKING_NUMBER AND bd.seq_number = t.BOOKING_SEQ Inner Join
	                 Bookings_Headline as bk on bk.Booking_Number = bd.Booking_Number Inner Join
	                 dbo.BLDTL AS bl ON bl.Booking_Number = bd.BOOKING_NUMBER AND bl.BK_Seq = bd.seq_number Inner Join
	                 BillOfLoadings as b on b.BL_Number = bl.BL_Number Inner Join
	                 CM_System as sh on sh.COMPANY_NUMBER = b.SHIPPER_NUMBER and sh.LOCATION_NUMBER = b.Shipper_Loc inner join
	                 CM_System as cg on cg.COMPANY_NUMBER = b.Cons_NUMBER and cg.LOCATION_NUMBER = b.Cons_Loc inner join
	                 CM_System as fw on fw.COMPANY_NUMBER = b.FWDR_NUMBER and fw.LOCATION_NUMBER = b.FWDR_Loc inner join
	                 CM_System as nf on nf.COMPANY_NUMBER = b.Notify_NUMBER and nf.LOCATION_NUMBER = b.Notify_Loc 
                  WHERE (t.Container = '" & Trim(Container) & "') AND (t.BOOKING_NUMBER <> 0) AND (ISNULL(bd.BL_Number, N'') <> '')
                ORDER BY bl.BL_Number DESC"

        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds
        Else
            Return ds
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Public Function Container_ID(ByVal Container As String) As Integer
        Dim ds As New DataSet
        Dim strSQL1 As String = "Select Container_ID From Equipment_Master Where Container = '" & Trim(Container) & "'"
        ds = ws.GetDataset(md.strConnect, strSQL1, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0).Item("Container_Id") > 0 Then
                Return ds.Tables(0).Rows(0).Item("Container_Id")
            Else
                Return 0
            End If
        End If
        strSQL1 = Nothing
    End Function

    Public Function Container_ID_From_Cont1_Cont2(ByVal Cont1 As String, ByVal Cont2 As Integer) As Integer
        Dim ds As New DataSet
        Dim strSQL1 As String = "Select Container_ID From Equipment_Master Where Container_no_1   = '" & Trim(Cont1) & "' and Container_no_2 = " & Cont2
        ds = ws.GetDataset(md.strConnect, strSQL1, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0).Item("Container_Id") > 0 Then
                Return ds.Tables(0).Rows(0).Item("Container_Id")
            Else
                Return 0
            End If
        End If
        strSQL1 = Nothing
    End Function

    Public Function Container_From_ID(ByVal ContainerID As Integer) As String
        Dim ds As New DataSet
        Dim strSQL1 As String = "Select Container From Equipment_Master Where Container_ID = " & Trim(ContainerID)
        ds = ws.GetDataset(md.strConnect, strSQL1, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return Trim(ds.Tables(0).Rows(0).Item("Container"))
        Else
            Return ""
        End If
        strSQL1 = Nothing
    End Function

    Public Function Container_Get_Cont1(ByVal Container As String) As String
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "Select isnull(Container_no_1,'') as Cont1 From Equipment_Master Where Container = '" & Trim(Container) & "'", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            If Len(Trim(ds.Tables(0).Rows(0).Item("Cont1"))) > 0 Then
                Return ds.Tables(0).Rows(0).Item("Cont1")
            Else
                Return ""
            End If
        Else
            Return ""
        End If
        ds = Nothing
    End Function

    Public Function Container_Get_Cont2(ByVal Container As String) As Integer
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "Select isnull(Container_no_2,0) as Cont2 From Equipment_Master Where Container = '" & Trim(Container) & "'", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            If Len(Trim(ds.Tables(0).Rows(0).Item("Cont2"))) > 0 Then
                Return ds.Tables(0).Rows(0).Item("Cont2")
            Else
                Return 0
            End If
        Else
            Return 0
        End If
        ds = Nothing
    End Function

    Public Function Container_RemoveLeadingZeroes(ByVal Container As String) As String
        Dim Cont1 As String = Mid(Container, 1, 4)
        Dim Cont2 As String = RemoveLeadingZeroes(Mid(Container, 6, 6))
        Dim Cont3 As String = Mid(Container, 13, 1)

        If Len(Trim(Cont3)) > 0 Then
            Return Trim(Cont1) & "-" & Trim(Cont2) & "-" & Trim(Cont3)
        Else
            Return Trim(Cont1) & "-" & Trim(Cont2)
        End If


    End Function

    Public Function Container_Get_Available(ByVal Container As String) As Boolean
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "Select isnull(Container,'') From Equipment_Master Where Container = '" & Trim(Container) & "' and rtrim(Current_Status) <> 'SOLD' and rtrim(Current_Status) <> 'HIST' and rtrim(Current_Status) <> 'RTD'", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            ds = Nothing
            Return True
        Else
            ds = Nothing
            Return False
        End If

    End Function

    Public Function Container_Get_Cont3(ByVal Container As String) As String
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "Select isnull(Container_no_3,'') as Cont3 From Equipment_Master Where Container = '" & Trim(Container) & "'", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            If Len(Trim(ds.Tables(0).Rows(0).Item("Cont3"))) > 0 Then
                Return ds.Tables(0).Rows(0).Item("Cont3")
            Else
                Return ""
            End If
        Else
            Return ""
        End If
        ds = Nothing
    End Function

    Public Function Container_Get_EqType(ByVal Container As String) As String
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "Select isnull(Equipment_Type,'') as EqType From Equipment_Master Where Container = '" & Trim(Container) & "'", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            If Len(Trim(ds.Tables(0).Rows(0).Item("EqType"))) > 0 Then
                Return ds.Tables(0).Rows(0).Item("EqType")
            Else
                Return ""
            End If
        Else
            Return ""
        End If
        ds = Nothing
    End Function

    Public Function Container_Get_EqType_ISO_Code(ByVal Type As String) As String
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "Select isnull(Equipment_Type,'') as EqType, isnull(ISO_Code,'') as Iso_Code From EqpMts Where Equipment_Type = '" & Trim(Type) & "'", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            If Len(Trim(ds.Tables(0).Rows(0).Item("ISO_Code"))) > 0 Then
                Return Trim(ds.Tables(0).Rows(0).Item("ISO_Code"))
            Else
                Return Trim(ds.Tables(0).Rows(0).Item("EqType"))
            End If
        Else
            Return ""
        End If
        ds = Nothing
    End Function

    Public Function Container_Get_MaxVol(ByVal Container As String) As Double
        Dim ds As New DataSet
        strSQL = "SELECT c.Container, c.EQUIPMENT_TYPE, isnull(e.MAX_VOLUME,0) as Max_Volume
                   FROM dbo.Equipment_Master AS c INNER JOIN
                        dbo.EqpMts AS e ON e.EQUIPMENT_TYPE = c.EQUIPMENT_TYPE
                  WHERE (c.Container = '" & Trim(Container) & "')"
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return Trim(ds.Tables(0).Rows(0).Item("Max_Volume"))
        Else
            Return 0
        End If
        ds = Nothing
    End Function

    Public Function Container_Get_CSC(ByVal ContainerID As Integer) As String
        Dim ds As New DataSet
        strSQL = "SELECT Top 1 isnull(VIN_Number,'') as CSC FROM CNTCSCVIN WHERE (Container_ID = " & ContainerID & ") ORDER BY CREATED_ON Desc , CREATED_AT DESC"
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return Trim(ds.Tables(0).Rows(0).Item("CSC"))
        Else
            Return ""
        End If
        ds = Nothing
    End Function

    Public Function Container_Get_CSC_Description(ByVal ContainerID As Integer) As String
        Dim ds As New DataSet
        strSQL = "SELECT Top 1 isnull(Description,'') as CSC_Desc FROM CNTINSP WHERE (Container_ID = " & ContainerID & ") ORDER BY Tran_Date Desc , Tran_Time DESC"
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return Trim(ds.Tables(0).Rows(0).Item("CSC_Desc"))
        Else
            Return ""
        End If
        ds = Nothing
    End Function

    Public Function Container_Get_CSC_Last_Mechanic(ByVal ContainerID As Integer) As String
        Dim ds As New DataSet
        strSQL = "SELECT Top 1 isnull(Mechanic,'') as Mechanic FROM CNTINSP Where container_id = " & ContainerID & " and insp_type = 'CSCINSP' ORDER BY Tran_date DESC"
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return Trim(ds.Tables(0).Rows(0).Item("Mechanic"))
        Else
            Return ""
        End If
        ds = Nothing
    End Function

    Public Function Container_Get_CSC_Expires(ByVal Manufacture As Date) As Date
        If Manufacture.Year < 6 Then
            Return Manufacture.AddYears(3)
        Else
            Return Manufacture.AddMonths(30)
        End If
    End Function

    Public Function Container_Get_MaxWeight(ByVal Container As String) As Double
        Dim ds As New DataSet
        strSQL = "SELECT c.Container, c.EQUIPMENT_TYPE, isnull(e.MAX_Weight,0) as Max_Weight
                   FROM dbo.Equipment_Master AS c INNER JOIN
                        dbo.EqpMts AS e ON e.EQUIPMENT_TYPE = c.EQUIPMENT_TYPE
                  WHERE (c.Container = '" & Trim(Container) & "')"
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return Trim(ds.Tables(0).Rows(0).Item("Max_Weight"))
        Else
            Return 0
        End If
        ds = Nothing
    End Function

    Function Get_Cont(ByVal Line As String, ByVal Param As Integer) As String
        Dim j As Integer = 0
        Dim pos1 As Integer = 0
        Dim pos2 As Integer = 0
        Dim pos As Integer = 0
        Dim vP As String = ""
        pos1 = InStr(Line, "-")
        pos = pos1
        Select Case Param
            Case 0
                vP = Mid(Line, 1, pos1 - 1)
            Case 1
                pos2 = InStr(pos1 + 1, Line, "-")
                If pos2 > 0 Then
                    vP = Mid(Line, pos1 + 1, pos2 - (pos1 + 1))
                Else
                    vP = Mid(Line, pos1 + 1, 6)
                End If
            Case 2
                pos2 = InStr(pos1 + 1, Line, "-")
                If pos2 > 0 Then
                    vP = Mid(Line, pos2 + 1, 1)
                End If
        End Select
        Return Trim(vP)
    End Function
#End Region

    Public Function New_User(ByVal UserName As String)
        Dim strSQL As String = "INSERT INTO dbo.UserDtl
                                      (UserCode, Menu, Modulo, [Desc], Accesso, activated, [Save], Edit, New, Copy, [Read],[Print])
                                    SELECT 2 AS UserCode, Menu, Modulo, [Desc], Accesso, activated, [Save], Edit, New, Copy, [Read], [Print]
                                    FROM   dbo.UserDtl AS UserDtl_1
                                    WHERE  (UserCode = 1)"
    End Function

    Public Function Equipment_Move(ByVal Yard As Integer, ByVal TIR As Integer, ByVal Cont_ID As Integer, ByVal Cont1 As String, ByVal Cont2 As Integer, ByVal Cont3 As String, ByVal Seal As String, ByVal Chas1 As String, ByVal Chas2 As Integer, ByVal Chas3 As String, ByVal Eq_Type As String, ByVal Tran_Date As Date, ByVal Tran_Time As String, ByVal Current_Status As String, ByVal UserName As String, ByVal Created_ON As Date, ByVal Source As String, ByVal Sailing As String, ByVal Send_To_Yard As String, ByVal BK As Integer, ByVal BK_Seq As Integer, ByVal PortL As Integer, ByVal PortD As Integer, ByVal Trucker As Integer, ByVal FileName As String, ByVal Note As String, ByVal nFrom As Integer, ByVal nTo As Integer)
        Dim nCont_ID As Integer = 0
        Dim uid As Integer = 1
        If Len(Trim(Cont2)) > 0 Then
            If Cont2 > 0 Then
                Dim ds As New DataSet
                If Cont_ID = 0 Then
                    ds = ws.GetDataset(md.strConnect, "Select Container_ID From Equipment_Master Where Container_no_1 = '" & Trim(Cont1) & "' and Container_no_2 = " & Cont2 & " and Container_No_3 = '" & Trim(Cont3) & "'", 1)
                    If ds.Tables(0).Rows.Count > 0 Then
                        nCont_ID = ds.Tables(0).Rows(0).Item("Container_ID")
                    End If
                Else
                    nCont_ID = Cont_ID
                End If
                Dim strContainer As String = md.Container_From_ID(nCont_ID)

                ds.Clear()
                ds = ws.GetDataset(md.strConnect, "Select top 1 uid From Eq_Movements Order By uid Desc", 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    uid = ds.Tables(0).Rows(0).Item("uid") + 1
                End If
                strSQL = "Insert Into Eq_Movements (LINE_NUMBER,STEVEDORE_NO,TIR_NUMBER
                          ,CONTAINER_ID,CONTAINER,Container_no_1,Container_no_2,Container_no_3,EQUIPMENT_TYPE,TRAN_DATE, Tran_Time
                          ,ACTIVITY_CODE,[USER],ENTRY_DATE                                              
                          ,Source, Sailing_nro,Port_Depart, Port_Dest, Trucker_Number,FileName,Note, From_ID,  To_ID, Booking_Number, Booking_Seq) Values (" & md.GL_Company & "," &
                          Yard & "," & TIR & "," &
                          nCont_ID & ",'" & Trim(strContainer) & "','" & Trim(Cont1) & "'," & Cont2 & ",'" & Trim(Cont3) & "','" & Trim(Eq_Type) & "','" &
                          Format(Tran_Date, "yyyy-MM-dd") & "','" & Trim(Tran_Time) & "','" &
                          Trim(Current_Status) & "','" &
                          Trim(UserName) & "','" &
                          Format(Created_ON, "yyyy-MM-dd") & "','" & Trim(Source) & "','" & Trim(Sailing) & "'," & PortL & "," & PortD & "," & Trucker & ",'" & Trim(FileName) & "','" & Trim(Note) & "'," & nFrom & "," & nTo & "," & BK & "," & BK_Seq & ")"
                md.eResp = ws.ExecSQL(md.strConnect, strSQL)
                Dim Eq_Movement_uid As Integer = md.Eq_Movement_last_uid

                ' ------- Update Equipment_Master
                ' ------- Change done by Carlos Bajo on 5/29/2018 ; Current Status "RCVF" => Chassis # in blank , to unliked Container and Chassis
                'If Trim(Current_Status) = "RCVF" Or Trim(Current_Status) = "FLD" Then
                '    strSQL = "Update Equipment_Master Set Current_status = '" & Trim(Current_Status) & "'" &
                '         ",Booking_Number = " & BK &
                '         ",TIR_Number = " & TIR &
                '         ",Stevedore = " & Yard &
                '         ",Sailing_nro = '" & Trim(Sailing) &
                '         "',Last_Activity = '" & Format(Tran_Date, "yyyy-MM-dd") &
                '         "',Sent_To_CY = '" & Trim(Send_To_Yard) &
                '         "',Chassis_1 = '',Chassis_2 = 0,Chassis_3 = '',Seal_Number = '" & Trim(Seal) & "'" &
                '         ",Trucker_Number = " & Trucker & "  Where Container_ID = " & nCont_ID
                'Else
                strSQL = "Update Equipment_Master Set Current_status = '" & Trim(Current_Status) & "'" &
                         ",Booking_Number = " & BK &
                         ",Booking_seq = " & BK_Seq &
                         ",TIR_Number = " & TIR &
                         ",Stevedore = " & Yard &
                         ",Sailing_nro = '" & Trim(Sailing) &
                         "',Last_Activity = '" & Format(Tran_Date, "yyyy-MM-dd") &
                         "',Sent_To_CY = '" & Trim(Send_To_Yard) &
                         "',Chassis_1 = '" & Trim(Chas1) &
                         "',Chassis_2 = " & Chas2 &
                         ",Chassis_3 = '" & Trim(Chas3) &
                         "',Seal_Number = '" & Trim(Seal) &
                         "',Trucker_Number = " & Trucker &
                         ",From_Number = " & nFrom &
                         ",To_Number = " & nTo &
                         ",Eq_Movement_uid = " & Eq_Movement_uid &
                " Where Container_ID = " & nCont_ID
                Eq_Movement_uid = Nothing

                'End If
                md.eResp = ws.ExecSQL(md.strConnect, strSQL)

                ' ------- Update BKHDT
                If BK > 0 And BK_Seq > 0 Then
                    If TIR <> 0 Then
                        strSQL = "Update Booking_Detail Set TIR = " & TIR & ",Yard = " & Yard & " Where Booking_number = " & BK & " and seq_number = " & BK_Seq
                        md.eResp = ws.ExecSQL(md.strConnect, strSQL)
                    End If
                End If
            End If
        End If
    End Function

    Public Function Eq_Movement_last_uid()
        Dim ds_uid As New DataSet
        ds_uid = ws.GetDataset(strConnect, "Select Top 1 uid From Eq_Movements ORDER BY uid DESC", 1)
        If ds_uid.Tables(0).Rows.Count > 0 Then
            Return ds_uid.Tables(0).Rows(0).Item("uid")
        Else
            Return 0
        End If
    End Function

    Public Function Chassis_Move(ByVal Yard As Integer, ByVal TIR As Integer, ByVal Cont_ID As Integer, ByVal Cont1 As String, ByVal Cont2 As Integer, ByVal Cont3 As String, ByVal Seal As String, ByVal Chas1 As String, ByVal Chas2 As Integer, ByVal Chas3 As String, ByVal Eq_Type As String, ByVal Tran_Date As Date, ByVal Tran_Time As String, ByVal Current_Status As String, ByVal UserName As String, ByVal Created_ON As Date, ByVal Source As String, ByVal Sailing As String, ByVal Send_To_Yard As String, ByVal BK As Integer, ByVal BK_Seq As Integer, ByVal PortL As Integer, PortD As Integer, Trucker As Integer, FileName As String, Note As String)
        Dim nCont_ID As Integer = 0
        Dim uid As Integer = 1
        If Len(Trim(Chas2)) > 0 Then
            If Chas2 > 0 Then
                Dim ds As New DataSet
                'If Cont_ID = 0 Then
                strSQL = "Select Container_ID From Equipment_Master Where Container_no_1 = '" & Trim(Chas1) & "' and Container_no_2 = " & Chas2 & " and Container_No_3 = '" & Trim(Chas3) & "'"
                ds = ws.GetDataset(md.strConnect, strSQL, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    nCont_ID = ds.Tables(0).Rows(0).Item("Container_ID")
                End If
                'Else
                '    nCont_ID = Cont_ID
                'End If
                ds.Clear()
                ds = ws.GetDataset(md.strConnect, "Select top 1 uid From Eq_Movements Order By uid Desc", 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    uid = ds.Tables(0).Rows(0).Item("uid") + 1
                End If
                strSQL = "Insert Into Eq_Movements (LINE_NUMBER,STEVEDORE_NO,TIR_NUMBER
                          ,CONTAINER_ID,CONTAINER,Container_no_1,Container_no_2,Container_no_3,EQUIPMENT_TYPE,TRAN_DATE, Tran_Time
                          ,ACTIVITY_CODE,[USER],ENTRY_DATE                                              
                          ,Source, Sailing_nro,Port_Depart, Port_Dest, Trucker_Number,FileName,Note) Values (" & md.GL_Company & "," &
                          Yard & "," & TIR & "," &
                          nCont_ID & ",'" & Trim(Chas1) & "-" & Trim(Str(Chas2)) & "-" & Trim(Chas3) & "','" & Trim(Chas1) & "'," & Chas2 & ",'" & Trim(Chas3) & "','" & Trim(Eq_Type) & "','" &
                          Format(Tran_Date, "yyyy-MM-dd") & "','" & Trim(Tran_Time) & "','" &
                          Trim(Current_Status) & "','" &
                          Trim(UserName) & "','" &
                          Format(Created_ON, "yyyy-MM-dd") & "','" & Trim(Source) & "','" & Trim(Sailing) & "'," & PortL & "," & PortD & "," & Trucker & ",'" & Trim(FileName) & "','" & Trim(Note) & "')"
                md.eResp = ws.ExecSQL(md.strConnect, strSQL)

                ' ------- Update Equipment_Master
                ' ------- Change done by Carlos Bajo on 7/17/2018 
                If Trim(Current_Status) = "RCVF" Or Trim(Current_Status) = "FLD" Then
                    strSQL = "Update Equipment_Master Set Current_status = '" & Trim(Current_Status) & "'" &
                             ",Booking_Number = " & BK &
                             ",TIR_Number = " & TIR &
                             ",Stevedore = " & Yard &
                             ",Sailing_nro = '" & Trim(Sailing) &
                             "',Last_Activity = '" & Format(Tran_Date, "yyyy-MM-dd") &
                             "',Sent_To_CY = '" & Trim(Send_To_Yard) &
                             "',Chassis_1 = '',Chassis_2 = 0,Chassis_3 = '',Seal_Number = ''" &
                             ",Trucker_Number = " & Trucker & "  Where Container_ID = " & nCont_ID
                Else
                    strSQL = "Update Equipment_Master Set Current_status = '" & Trim(Current_Status) & "'" &
                             ",Booking_Number = " & BK &
                             ",TIR_Number = " & TIR &
                             ",Stevedore = " & Yard &
                             ",Sailing_nro = '" & Trim(Sailing) &
                             "',Last_Activity = '" & Format(Tran_Date, "yyyy-MM-dd") &
                             "',Sent_To_CY = '" & Trim(Send_To_Yard) &
                             "',Chassis_1 = '" & Trim(Cont1) &
                             "',Chassis_2 = " & Cont2 &
                             ",Chassis_3 = '" & Trim(Cont3) &
                             "',Seal_Number = '" & Trim(Seal) &
                             "',Trucker_Number = " & Trucker &
                             " Where Container_ID = " & nCont_ID
                End If
                md.eResp = ws.ExecSQL(md.strConnect, strSQL)

            End If
        End If
    End Function

#Region "Booking Get Info"
    ' ----------------- Function Created after 4/27/2018, New version
    Public Function Booking_Get_Trucker_Number(ByVal BK As Integer) As Integer
        Dim Trucker_Number As Integer = 0
        Dim strSQL As String = "SELECT TOP (1) dr.COMPANY_Number as Trucker_Number
                                    FROM  Bookings_Headline AS b INNER JOIN
                                          CM_System AS dr ON b.TRUCKER_NUMBER = dr.COMPANY_NUMBER AND b.TRUCKER_LOC = dr.LOCATION_NUMBER
                                    WHERE (b.BOOKING_NUMBER = " & BK & ")
                                    ORDER BY b.BOOKING_DATE DESC"
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Trucker_Number = Trim(ds.Tables(0).Rows(0).Item("Trucker_Number"))
        End If
        ds = Nothing
        strSQL = Nothing
        Return Trim(Trucker_Number)
    End Function
    Public Function Booking_Get_Trucker_Name(ByVal BK As Integer) As String
        Dim Trucker_Name As String = ""
        Dim strSQL As String = "SELECT TOP (1) dr.COMPANY_NAME as Trucker_Name
                                    FROM  Bookings_Headline AS b INNER JOIN
                                          CM_System AS dr ON b.TRUCKER_NUMBER = dr.COMPANY_NUMBER AND b.TRUCKER_LOC = dr.LOCATION_NUMBER
                                    WHERE (b.BOOKING_NUMBER = " & BK & ")
                                    ORDER BY b.BOOKING_DATE DESC"
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Trucker_Name = Trim(ds.Tables(0).Rows(0).Item("Trucker_Name"))
        End If
        ds = Nothing
        strSQL = Nothing
        Return Trim(Trucker_Name)
    End Function
    Public Function Booking_Get_Port_Loading_Number(ByVal BK As Integer) As Integer
        Dim Port_Number As Integer = 0
        Dim strSQL As String = "SELECT TOP (1) p.PORT_Number
                                    FROM Bookings_Headline AS b INNER JOIN
                                         Ports AS p ON b.PORT_LOADING = p.PORT_NUMBER
                               WHERE (b.BOOKING_NUMBER = " & BK & ")"
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Port_Number = Trim(ds.Tables(0).Rows(0).Item("Port_Number"))
        End If
        ds = Nothing
        strSQL = Nothing
        Return Trim(Port_Number)
    End Function
    Public Function Booking_Get_PortT_Int_Code(ByVal BK As Integer) As String
        Dim Port_Int_Code As String = ""
        Dim strSQL As String = "SELECT TOP (1) p.Port_Int_Code
                                    FROM Bookings_Headline AS b INNER JOIN
                                         Ports AS p ON b.PORT_Transh = p.PORT_NUMBER
                               WHERE (b.BOOKING_NUMBER = " & BK & ")"
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Port_Int_Code = Trim(ds.Tables(0).Rows(0).Item("Port_Int_Code"))
        End If
        ds = Nothing
        strSQL = Nothing
        Return Trim(Port_Int_Code)
    End Function
    Public Function Booking_Get_Port_Loading(ByVal BK As Integer) As String
        Dim Port_Name As String = ""
        Dim strSQL As String = "SELECT TOP (1) p.PORT_NAME
                                    FROM Bookings_Headline AS b INNER JOIN
                                         Ports AS p ON b.PORT_LOADING = p.PORT_NUMBER
                               WHERE (b.BOOKING_NUMBER = " & BK & ")"
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Port_Name = Trim(ds.Tables(0).Rows(0).Item("Port_Name"))
        End If
        ds = Nothing
        strSQL = Nothing
        Return Trim(Port_Name)
    End Function
    Public Function Booking_Get_Port_Discharge_Number(ByVal BK As Integer) As Integer
        Dim Port_Number As Integer = 0
        Dim strSQL As String = "SELECT TOP (1) p.PORT_Number
                                    FROM Bookings_Headline AS b INNER JOIN
                                         Ports AS p ON b.PORT_Discharge = p.PORT_NUMBER
                               WHERE (b.BOOKING_NUMBER = " & BK & ")"
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Port_Number = Trim(ds.Tables(0).Rows(0).Item("Port_Number"))
        End If
        ds = Nothing
        strSQL = Nothing
        Return Trim(Port_Number)
    End Function
    Public Function Booking_Get_Port_Discharge(ByVal BK As Integer) As String
        Dim Port_Name As String = ""
        Dim strSQL As String = "SELECT TOP (1) p.PORT_NAME
                                    FROM Bookings_Headline AS b INNER JOIN
                                         Ports AS p ON b.PORT_Discharge = p.PORT_NUMBER
                               WHERE (b.BOOKING_NUMBER = " & BK & ")"
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Port_Name = Trim(ds.Tables(0).Rows(0).Item("Port_Name"))
        End If
        ds = Nothing
        strSQL = Nothing
        Return Trim(Port_Name)
    End Function
    Public Function Booking_Get_Port_Transshipment(ByVal BK As Integer) As Integer
        Dim Port_Number As Integer = 0
        Dim strSQL As String = "SELECT TOP (1) isnull(PORT_Transshipment,1) as Port_Transshipment FROM Bookings_Headline WHERE (BOOKING_NUMBER = " & BK & ")"
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Port_Number = Trim(ds.Tables(0).Rows(0).Item("Port_Transshipment"))
        End If
        ds = Nothing
        strSQL = Nothing
        Return Trim(Port_Number)
    End Function
    ' ------- Get Sailing info from BK
    Public Function Booking_Get_Sailing(ByVal BK As Integer) As String
        Dim Sailing As String = ""
        Dim strSQL As String = "SELECT Sailing_nro FROM Bookings_Headline Where Booking_Number = " & BK
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Sailing = Trim(ds.Tables(0).Rows(0).Item("Sailing_nro"))
        End If
        ds = Nothing
        strSQL = Nothing
        Return Trim(Sailing)
    End Function
    ' ------- Get Container from Yard, BK
    Public Function Booking_Get_Container(ByVal BK As Integer) As String
        Dim Container As String = ""
        Dim strSQL As String = "SELECT isnull(Container,'') as Container FROM TIR_CNT_Transfer Where DR_Stevedoring = " & md.Warehouse_D & " and Booking_Number = " & BK
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Container = Trim(ds.Tables(0).Rows(0).Item("Container"))
        End If
        ds = Nothing
        strSQL = Nothing
        Return Trim(Container)
    End Function

    Public Function Booking_Get_Vessel(ByVal BK As Integer) As String
        Dim Vessel_Name As String = ""
        Dim strSQL As String = "SELECT s.Vessel_Name FROM Bookings_Headline as bk Inner Join 
                                    Sailing_Master as s on bk.Sailing_nro = s.Sailing_nro and bk.Port_Loading = s.Port 
                                Where bk.Booking_Number = " & BK
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Vessel_Name = Trim(ds.Tables(0).Rows(0).Item("Vessel_Name"))
        End If
        ds = Nothing
        strSQL = Nothing
        Return Trim(Vessel_Name)
    End Function
    Public Function Booking_Get_South_or_NorthBound(ByVal PortL As Integer) As String
        Dim ds As New DataSet
        If PortL > 0 Then
            ds = ws.GetDataset(md.strConnect, "Select isnull(Country,'') as Country From Ports Where Port_number = " & PortL, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                If Trim(ds.Tables(0).Rows(0).Item("Country")) = "U.S.A." Then
                    Return "Southbound"
                Else
                    Return "Northbound"
                End If
            End If
        End If
        ds = Nothing
    End Function
    Public Function Booking_Get_Voyage(ByVal BK As Integer) As String
        Dim Voyage_nro As String = ""
        Dim strSQL As String = "SELECT s.Voyage_nro FROM Bookings_Headline as bk Inner Join 
                                    Sailing_Master as s on bk.Sailing_nro = s.Sailing_nro and bk.Port_Loading = s.Port 
                                Where bk.Booking_Number = " & BK
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Voyage_nro = Trim(ds.Tables(0).Rows(0).Item("Voyage_nro"))
        End If
        ds = Nothing
        strSQL = Nothing
        Return Trim(Voyage_nro)
    End Function
    Public Function Booking_Get_Temperature(ByVal BK As Integer, ByVal seq As Integer) As String
        Dim Temp As String = ""
        Dim strSQL As String = "SELECT Temp_From FROM Booking_Detail Where Booking_Number = " & BK & " and seq_number = " & seq
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Temp = Trim(ds.Tables(0).Rows(0).Item("Temp_From"))
        End If
        ds = Nothing
        strSQL = Nothing
        Return Trim(Temp)
    End Function
    Public Function Booking_Get_Temp_Type(ByVal BK As Integer, ByVal seq As Integer) As String
        Dim Temp_Type As String = ""
        Dim strSQL As String = "SELECT Temp_Type FROM Booking_Detail Where Booking_Number = " & BK & " and seq_number = " & seq
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Temp_Type = Trim(ds.Tables(0).Rows(0).Item("Temp_Type"))
        End If
        ds = Nothing
        strSQL = Nothing
        If Temp_Type = "F" Then
            Temp_Type = "FAH"
        End If
        Return Trim(Temp_Type)
    End Function
    Public Function Booking_Get_Transshipment(ByVal BK As Integer) As Integer
        strSQL = "SELECT isnull(BK_Transshipment,0) as BK_Transshipment FROM Bookings_Headline Where Booking_Number = " & BK
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return Trim(ds.Tables(0).Rows(0).Item("BK_transshipment"))
        Else
            Return 0
        End If
        ds = Nothing
    End Function
    Public Function Booking_Get_Transshipment_Yes_Not(ByVal BK As Integer, ByVal PortL As Integer) As Boolean
        strSQL = "SELECT isnull(BK_Transshipment,0) as BK_Transshipment FROM Bookings_Headline Where Booking_Number = " & BK & " and Port_Transshipment = " & PortL
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
        ds = Nothing
    End Function
    Public Function Booking_Get_Est_Departure(ByVal BK As Integer) As Date
        Dim Departure As Date = Format(System.DateTime.Today, "MM/dd/yyyy")
        Dim strSQL As String = "SELECT Format(s.Est_departure,'MM/dd/yyyy') as Est_Departure FROM Bookings_Headline as bk Inner Join 
                                    Sailing_Master as s on bk.Sailing_nro = s.Sailing_nro and bk.Port_Loading = s.Port 
                                Where bk.Booking_Number = " & BK
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            If Not IsDBNull(ds.Tables(0).Rows(0).Item("Est_Departure")) Then
                Departure = Trim(ds.Tables(0).Rows(0).Item("Est_Departure"))
            End If
        End If
        ds = Nothing
        strSQL = Nothing
        Return Format(Departure, "MM/dd/yyyy")
    End Function

    ' ------- Get Terminal info from BK
    Public Function Booking_Get_Terminal(ByVal BK As Integer) As String
        Dim Terminal As String = ""
        Dim strSQL As String = "SELECT c.COMPANY_NAME AS Terminal_name
                                FROM Bookings_Headline AS bk INNER JOIN
                                     CM_System AS c ON c.COMPANY_NUMBER = bk.STEVEDORE_NO AND c.LOCATION_NUMBER = 0
                                WHERE (bk.BOOKING_NUMBER = " & BK & ")"
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Terminal = Trim(ds.Tables(0).Rows(0).Item("Terminal_Name"))
        End If
        ds = Nothing
        strSQL = Nothing
        Return Trim(Terminal)
    End Function
    Public Function Booking_Get_Terminal_City(ByVal BK As Integer) As String
        Dim Terminal_City As String = ""
        Dim strSQL As String = "SELECT isnull(c.City,'') as Terminal_City
                                FROM Bookings_Headline AS bk INNER JOIN
                                     CM_System AS c ON c.COMPANY_NUMBER = bk.STEVEDORE_NO AND c.LOCATION_NUMBER = 0
                                WHERE (bk.BOOKING_NUMBER = " & BK & ")"
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Terminal_City = Trim(ds.Tables(0).Rows(0).Item("Terminal_City"))
        End If
        ds = Nothing
        strSQL = Nothing
        Return Trim(Terminal_City)
    End Function
    Public Function Booking_Get_Terminal_Country(ByVal BK As Integer) As String
        Dim Terminal_Country As String = ""
        Dim strSQL As String = "SELECT  isnull(c.Country,'') as Terminal_Country
                                FROM Bookings_Headline AS bk INNER JOIN
                                     CM_System AS c ON c.COMPANY_NUMBER = bk.STEVEDORE_NO AND c.LOCATION_NUMBER = 0
                                WHERE (bk.BOOKING_NUMBER = " & BK & ")"
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Terminal_Country = Trim(ds.Tables(0).Rows(0).Item("Terminal_Country"))
        End If
        ds = Nothing
        strSQL = Nothing
        Return Trim(Terminal_Country)
    End Function

    ' ------- Get Stuffing Info from BK
    Public Function Booking_Get_Stuffed_ID(ByVal BK As Integer) As Integer
        strSQL = "SELECT uid as Stuffed_ID FROM dbo.STFHDR Where Booking_Number = " & BK
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return Trim(ds.Tables(0).Rows(0).Item("Stuffed_ID"))
        Else
            Return 0
        End If
        ds = Nothing
    End Function

    ' ------- Get Stuffing Posted
    Public Function Booking_Get_Stuffed_Posted(ByVal BK As Integer) As String
        strSQL = "SELECT uid as Stuffed_ID, isnull(Posted,'') as Posted FROM dbo.STFHDR Where Booking_Number = " & BK & " and isnull(Posted,'') = 'P'"
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return "Y"
        Else
            Return "N"
        End If
        ds = Nothing
    End Function

    ' ------- HazMat Line HazMat ?????
    Public Function Booking_HazMat_Line(ByVal BK As Integer, Line As Integer) As String
        strSQL = "SELECT isNull(DC_Flag,'') as HazMat FROM dbo.Booking_Detail Where Booking_Number = " & BK & " and seq_number = " & Line & " and isnull(DC_Flag,'') = 'Y'"
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return "Y"
        Else
            Return "N"
        End If
        ds = Nothing
    End Function

    ' ------- HazMat info
    Public Function Booking_HazMat_Info(ByVal BK As Integer, Line As Integer) As String
        strSQL = "SELECT TOP (1) UN_NUMBER FROM dbo.Booking_Hazardous Where Booking_Number = " & BK & " and seq_number = " & Line
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return "Y"
        Else
            Return "N"
        End If
        ds = Nothing
    End Function

    ' ------- Bonded info
    Public Function Booking_Bonded_Info(ByVal BK As Integer, Line As Integer) As String
        strSQL = "SELECT TOP (1) InBond_Number FROM dbo.Booking_Bonded Where Booking_Number = " & BK & " and Booking_Line = " & Line
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return "Y"
        Else
            Return "N"
        End If
        ds = Nothing
    End Function

    Public Function Booking_Canceled(ByVal Booking As Integer) As Boolean
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, "SELECT BOOKING_NUMBER, Cancelled FROM dbo.Bookings_Headline WHERE (ISNULL(Cancelled, '') = 'Y') AND (BOOKING_NUMBER = " & Booking & ")", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region

    Public Function CheckApostrophe_New(ByVal StrToCheck As String, ByVal StrChecked As String)
        StrChecked = StrChecked.Replace("'", "''")
        Return (StrChecked)
    End Function

    Public Function GetMod11CheckDigit(ByVal strNumber As String) As String
        Dim i As Integer
        Dim l As Integer
        Dim intAccumulator As Integer
        Dim intMultiplicand As Integer
        Dim intModVal As Integer

        strNumber = Trim(strNumber)
        intAccumulator = 0
        intMultiplicand = 2
        l = Len(strNumber)
        For i = l To 1 Step -1
            intAccumulator = intAccumulator + CInt(Mid(strNumber, i, 1)) * intMultiplicand
            intMultiplicand = intMultiplicand + 1
        Next
        If intAccumulator Mod 11 > 0 Then
            intModVal = 11 - intAccumulator Mod 11
        Else
            intModVal = 0
        End If

        If intModVal = 10 Then
            GetMod11CheckDigit = "X"
        Else
            GetMod11CheckDigit = CStr(intModVal)
        End If
    End Function

    Public Function Convert_CHR_To_Num(ByVal vChar As Char) As Integer
        Select Case vChar
            Case "A"
                Return 10
            Case "B"
                Return 12
            Case "C"
                Return 13
            Case "D"
                Return 14
            Case "E"
                Return 15
            Case "F"
                Return 16
            Case "G"
                Return 17
            Case "H"
                Return 18
            Case "I"
                Return 19
            Case "J"
                Return 20
            Case "K"
                Return 21
            Case "L"
                Return 23
            Case "M"
                Return 24
            Case "N"
                Return 25
            Case "O"
                Return 26
            Case "P"
                Return 27
            Case "Q"
                Return 28
            Case "R"
                Return 29
            Case "S"
                Return 30
            Case "T"
                Return 31
            Case "U"
                Return 32
            Case "V"
                Return 34
            Case "W"
                Return 35
            Case "X"
                Return 36
            Case "Y"
                Return 37
            Case "Z"
                Return 38
        End Select
    End Function

    Public Function GetCheckDig(ByVal strNumber As String) As String
        If Len(Trim(strNumber)) = 10 Then
            Dim vChk1 As String = ""
            Dim vChk2 As String = ""
            Dim vChk3 As String = ""
            Dim vChk4 As String = ""
            Dim nChk As Integer = 0
            Dim nChk_Sum As Integer = 0
            Dim nChk1 As Integer = 0
            Dim nChk2 As Integer = 0
            Dim nChk3 As Integer = 0
            Dim nChk4 As Integer = 0
            Dim nChk5 As Integer = 0
            Dim nChk6 As Integer = 0
            Dim nChk7 As Integer = 0
            Dim nChk8 As Integer = 0
            Dim nChk9 As Integer = 0
            Dim nChk10 As Integer = 0

            vChk1 = Mid(strNumber, 1, 1)
            vChk2 = Mid(strNumber, 2, 1)
            vChk3 = Mid(strNumber, 3, 1)
            vChk4 = Mid(strNumber, 4, 1)



            nChk1 = CInt(Convert_CHR_To_Num(Mid(strNumber, 1, 1))) * 1
            nChk2 = CInt(Convert_CHR_To_Num(Mid(strNumber, 2, 1))) * 2
            nChk3 = CInt(Convert_CHR_To_Num(Mid(strNumber, 3, 1))) * 4
            nChk4 = CInt(Convert_CHR_To_Num(Mid(strNumber, 4, 1))) * 8
            nChk5 = CInt(Mid(strNumber, 5, 1)) * 16
            nChk6 = CInt(Mid(strNumber, 6, 1)) * 32
            nChk7 = CInt(Mid(strNumber, 7, 1)) * 64
            nChk8 = CInt(Mid(strNumber, 8, 1)) * 128
            nChk9 = CInt(Mid(strNumber, 9, 1)) * 256
            nChk10 = CInt(Mid(strNumber, 10, 1)) * 512

            nChk_Sum = nChk1 + nChk2 + nChk3 + nChk4 + nChk5 + nChk6 + nChk7 + nChk8 + nChk9 + nChk10
            nChk = Int(nChk_Sum / 11) * 11
            nChk = nChk_Sum - nChk
            If nChk = 10 Then
                nChk = 0
            End If
            GetCheckDig = CStr(nChk)
        Else
            GetCheckDig = ""
        End If
        'MsgBox("Dig: " & nChk)
    End Function

    Public Function GetMod10CheckDigit(ByVal strNumber As String) As String
        ' ----- The module 10 check digit routine is the current routine used 
        ' ----- to calculate the check digit for the Bookland EAN.
        Dim i As Integer
        Dim l As Integer
        Dim intAccumulator As Integer
        Dim intMultiplicand As Integer
        Dim intModVal As Integer

        strNumber = Trim(strNumber)
        intAccumulator = 0
        intMultiplicand = 2
        l = Len(strNumber)

        Dim MySum As Double = (Val(Mid(strNumber, 1, 1)) * 1) + (Val(Mid(strNumber, 2, 1)) * 3) + (Val(Mid(strNumber, 3, 1)) * 1) + (Val(Mid(strNumber, 4, 1)) * 3) + (Val(Mid(strNumber, 5, 1)) * 1) + (Val(Mid(strNumber, 6, 1)) * 3) + (Val(Mid(strNumber, 7, 1)) * 1) + (Val(Mid(strNumber, 8, 1)) * 3) + (Val(Mid(strNumber, 9, 1)) * 1) + (Val(Mid(strNumber, 10, 1)) * 3) + (Val(Mid(strNumber, 11, 1)) * 1) + (Val(Mid(strNumber, 12, 1)) * 3)
        Dim MyRemainder As Integer = Int(MySum Mod 10)
        Dim MyCheckDig As Integer = 10 - MyRemainder

        For i = l To 1 Step -1
            intAccumulator = intAccumulator + CInt(Mid(strNumber, i, 1)) * intMultiplicand
            intMultiplicand = intMultiplicand + 1
        Next
        intAccumulator = MySum
        If intAccumulator Mod 11 > 0 Then
            intModVal = 10 - intAccumulator Mod 10
        Else
            intModVal = 0
        End If

        If intModVal = 10 Then
            GetMod10CheckDigit = "X"
        Else
            GetMod10CheckDigit = CStr(intModVal)
        End If
    End Function

    Public Function Chassis_UnLink(ByVal Chassis1 As String, ByVal Chassis2 As Integer) As String
        strSQL = "UPDATE Equipment_Master SET CHASSIS_1 = '', CHASSIS_2 = 0 WHERE (CHASSIS_2 = " & Chassis2 & ") AND (CHASSIS_1 = '" & Trim(Chassis1) & "')"
        eResp = ws.ExecSQL(strConnect, strSQL)
    End Function

    Public Function Chassis_Available_Old2(ByVal Container As String, ByVal Chas_1 As String, ByVal Chas_2 As Integer, ByVal Chas_3 As String) As String
        If Len(Trim(Chas_1)) = 0 And Chas_2 = 0 Then
            Return "Available"
            Exit Function
        End If
        Dim ds As New DataSet
        'eResp = "Select Container_ID,Container,isnull(Current_Status,'') as Current_Status  From Equipment_Master Where (Chassis_1 = '" & Trim(Chas_1) & "' and Chassis_2 = " & Chas_2 & " and Chassis_3 = '" & Trim(Chas_3) & "') and Current_Status <> 'RTD' and Current_Status <> 'SOLD' and Current_Status <> 'HIST'  Order by LAST_ACTIVITY Desc"
        eResp = "Select Top 1 Container_ID,Container,isnull(Current_Status,'') as Current_Status  From Equipment_Master Where (Chassis_1 = '" & Trim(Chas_1) & "' and Chassis_2 = " & Chas_2 & " and Chassis_3 = '" & Trim(Chas_3) & "') and Current_Status <> 'RTD' and Current_Status <> 'SOLD' and Current_Status <> 'HIST'  Order by LAST_ACTIVITY Desc"
        ds = ws.GetDataset(md.strConnect, eResp, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            If Trim(ds.Tables(0).Rows(0).Item("Current_Status")) = "SNTE" Or Trim(ds.Tables(0).Rows(0).Item("Current_Status")) = "SNTF" Then
                If Trim(Container) = Trim(ds.Tables(0).Rows(0).Item("Container")) Then
                    Return "Available"
                Else
                    Return Trim(ds.Tables(0).Rows(0).Item("Container"))
                End If
            Else
                Return "Available"
            End If
        Else
            Return "Available"
        End If
        ds = Nothing
    End Function

    Public Function Chassis_Available(ByVal Container As String, ByVal Chas_1 As String, ByVal Chas_2 As Integer, ByVal Chas_3 As String) As String
        If Len(Trim(Chas_1)) = 0 And Chas_2 = 0 Then
            Return "Available"
            Exit Function
        End If
        Dim ds As New DataSet
        'eResp = "Select Container_ID,Container,isnull(Current_Status,'') as Current_Status  From Equipment_Master Where (Chassis_1 = '" & Trim(Chas_1) & "' and Chassis_2 = " & Chas_2 & " and Chassis_3 = '" & Trim(Chas_3) & "') and Current_Status <> 'RTD' and Current_Status <> 'SOLD' and Current_Status <> 'HIST'  Order by LAST_ACTIVITY Desc"
        eResp = "Select Top 1 Container_ID,Container, isnull(Chassis_1,'') as Chassis_1, isnull(Chassis_2,0) as Chassis_2, isnull(Chassis_3,'') as Chassis_3  ,isnull(Current_Status,'') as Current_Status  From Equipment_Master Where (Container_no_1 = '" & Trim(Chas_1) & "' and Container_no_2 = " & Chas_2 & " and Container_no_3 = '" & Trim(Chas_3) & "') and Current_Status <> 'RTD' and Current_Status <> 'SOLD' and Current_Status <> 'HIST'  Order by LAST_ACTIVITY Desc"
        ds = ws.GetDataset(md.strConnect, eResp, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            If Trim(ds.Tables(0).Rows(0).Item("Chassis_1")) = "" And ds.Tables(0).Rows(0).Item("Chassis_2") = 0 And Trim(ds.Tables(0).Rows(0).Item("Chassis_3")) = "" Then
                Return "Available"
            Else
                Dim vCont As String = ""
                vCont = Trim(ds.Tables(0).Rows(0).Item("Chassis_1")) & "-" & Trim(Str(ds.Tables(0).Rows(0).Item("Chassis_2"))) & "-" & Trim(ds.Tables(0).Rows(0).Item("Chassis_3"))
                If Trim(Container) = Trim(vCont) Then
                    Return "Available"
                Else
                    MsgBox("This Chassis is linked to the Container : " & Trim(ds.Tables(0).Rows(0).Item("Chassis_1")) & "-" & Trim(Str(ds.Tables(0).Rows(0).Item("Chassis_2"))) & Trim(ds.Tables(0).Rows(0).Item("Chassis_3")))
                    Dim style As MsgBoxStyle
                    Dim response As MsgBoxResult
                    style = MsgBoxStyle.DefaultButton2 Or MsgBoxStyle.Critical Or MsgBoxStyle.YesNo
                    response = MsgBox("This Chassis Is linked to the Container :  " & Trim(vCont) & ", Are you sure, you want unlink this chassis?", style, "Warning")
                    If response = MsgBoxResult.Yes Then   ' User chose Yes
                        vCont = Trim(ds.Tables(0).Rows(0).Item("Chassis_1")) & "-" & Trim(Str(ds.Tables(0).Rows(0).Item("Chassis_2"))) & "-" & Trim(ds.Tables(0).Rows(0).Item("Chassis_3"))
                        strSQL = "UPDATE Equipment_Master SET CHASSIS_1 = '', CHASSIS_2 = 0, Chassis_3 = ''  Where (Container_no_1 = '" & Trim(Chas_1) & "' and Container_no_2 = " & Chas_2 & " and Container_no_3 = '" & Trim(Chas_3) & "')"
                        eResp = ws.ExecSQL(strConnect, strSQL)
                        Return "Available"
                    Else
                        Return Trim(ds.Tables(0).Rows(0).Item("Chassis_1") & "-" & Trim(Str(ds.Tables(0).Rows(0).Item("Chassis_2"))) & Trim(ds.Tables(0).Rows(0).Item("Chassis_3")))
                    End If
                End If
            End If
        Else
            Return "Available"
        End If
        ds = Nothing
    End Function

    Public Function Chassis_Available_Old1(ByVal Chas_1 As String, ByVal Chas_2 As Integer, ByVal Chas_3 As String, ByVal Cont_1 As String, ByVal Cont_2 As Integer, ByVal Cont_3 As String) As String
        If Len(Trim(Chas_1)) = 0 And Chas_2 = 0 Then
            Return "Available"
            Exit Function
        End If
        Dim ds As New DataSet
        eResp = "Select Container_ID,Container,isnull(Current_Status,'') as Current_Status  From Equipment_Master Where (Chassis_1 = '" & Trim(Chas_1) & "' and Chassis_2 = " & Chas_2 & " and Chassis_3 = '" & Trim(Chas_3) & "') and (Container_no_1 <> '" & Trim(Cont_1) & "' and Container_no_2 <> " & Cont_2 & " and Container_no_3 <> '" & Trim(Cont_3) & "') and Current_Status <> 'RTD' and Current_Status <> 'SOLD' and Current_Status <> 'HIST'  Order by LAST_ACTIVITY Desc"
        ds = ws.GetDataset(md.strConnect, eResp, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            If Trim(ds.Tables(0).Rows(0).Item("Current_Status")) = "SNTE" Or Trim(ds.Tables(0).Rows(0).Item("Current_Status")) = "SNTF" Then
                Return Trim(ds.Tables(0).Rows(0).Item("Container"))
            Else
                Return "Available"
            End If
        Else
            Return "Available"
        End If
        ds = Nothing
    End Function
    Public Function Insert_User_Log(ByVal vDesc As String, ByVal UserName As String) As String
        ' ------- ACCOUNT JOURNAL -------------------------------------------------------
        Try
            Dim strHostName As String
            Dim strIPAddress As String = ""

            strHostName = System.Net.Dns.GetHostName()

            strIPAddress = System.Net.Dns.GetHostByName(strHostName).AddressList(0).ToString()


            Dim strSQL As String = "Insert into User_Tracking_Log (strConnection, Line, Tracking_Now, Description, User_Domain, UserName, User_Machine, MachineName,HostName,strIPAddress
                          ) values ('" & Trim(strConnect) & "','" & Trim(md.GL_Company_Name) & "','" & Format(System.DateTime.Now, "yyyy/MM/dd hh:mm:ss") & "','" & Trim(Replace(Mid(vDesc, 1, 150), "'", "''")) & "','" & Trim(Mid(System.Environment.UserDomainName, 1, 30)) & "','" & Trim(Mid(UserName, 1, 30)) & "','" & Trim(Mid(System.Environment.UserName, 1, 30)) & "','" & Trim(Mid(System.Environment.MachineName, 1, 30)) & "','" & Trim(Mid(strHostName, 1, 30)) & "','" & Trim(Mid(strIPAddress, 1, 50)) & "')"
            'MsgBox(md.strConnect)
            'MsgBox(strSQL)
            md.eResp = ws.ExecSQL(md.strConnect, strSQL)
            Return "Success"
            strHostName = Nothing
            strIPAddress = Nothing
        Catch ex As Exception
            MsgBox(ex.Message)
            Return Trim(ex.Message)
        End Try
    End Function

#Region "email"
    Public Sub Send_Mail_exanche(ByVal vTo As String, ByVal vFrom As String, ByVal vSubject As String, ByVal vBody As String)
        Try
            Dim mailMsg As New MailMessage(vFrom, vTo)
            With mailMsg
                .Subject = vSubject
                .Body = vBody
            End With

            Try
                Dim client As New SmtpClient("smtp.gmail.com")
                ' ("amtext01")
                client.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis
                client.Send(mailMsg)

                MessageBox.Show("Your email has been successfully sent!", "Email Send Status", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch exp As Exception
                'Me.txtmsg.Text = exp.Message.ToString
                MessageBox.Show("The following problem occurred when attempting to " &
                    "send your email: " & exp.Message,
                    "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            'Dim myMailClient As New System.Net.Mail.SmtpClient("CARLOSMBAJO-PC")
            'myMailClient.DeliveryMethod = SmtpDeliveryMethod.PickupDirectoryFromIis
            'myMailClient.Send(vFrom, vTo, vSubject, vBody)
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Sub

    Public Sub Send_Mail_gmail(ByVal vTo As String, ByVal vFrom As String, ByVal vSubject As String, ByVal vBody As String)
        Try
            Dim Mail As New MailMessage(vFrom, "carlos.ramirez@kingocean.com")
            Dim SMTP As New SmtpClient()
            SMTP.Host = "smtp.gmail.com"
            SMTP.Port = 587
            SMTP.EnableSsl = True
            SMTP.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network
            SMTP.UseDefaultCredentials = False

            Mail.Subject = "Test AR"
            'Mail.From = New MailAddress("cdsoftdeveloper@gmail.com")
            SMTP.Credentials = New System.Net.NetworkCredential("cdsoftdeveloper@gmail.com", "atb0n2zc") '<-- Password Here

            Mail.Body = "" 'Message Here


            SMTP.Send(Mail)
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Sub

    Public Sub Insert_Error_msg(ByVal vProgram As String, ByVal vmsg As String, vSQL As String)
        strSQL = "Insert Into MSG_Erros (Created_Date, Program, msg, strSQL) Values ('" & System.DateTime.Now & "','" & Trim(vProgram) & "','" & Trim(vmsg) & "','" & Trim(vSQL) & "')"
        ws.ExecSQL(md.strConnect, strSQL)

    End Sub

#End Region

    Public Function BL_Type_To_Rate_Search(ByVal BL As String) As String
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "Select isnull(Move_Type,'LCL') as Move_Type From BillOfLoadings Where BL_Number = '" & Trim(BL) & "'", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0).Item("Move_Type") = "LCL" Then
                Return "DR"
            Else
                Return "BK"
            End If
        Else
            Return ""
        End If
    End Function

    Public Function BL_Type_BK_DR_Search(ByVal BL As String) As String
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "Select isnull(Booking_Number,0) as Booking_Number, isnull(DR_Number,0) as DR_Number From BillOfLoadings Where BL_Number = '" & Trim(BL) & "'", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0).Item("Booking_Number") > 0 Then
                Return "BK"
            Else
                If ds.Tables(0).Rows(0).Item("DR_Number") > 0 Then
                    Return "DR"
                Else
                    Return ""
                End If
            End If
        Else
            Return ""
        End If
    End Function

    Public Function BL_DTL_Type_Search(ByVal BL As String, ByVal seq As Integer) As String
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "Select isnull(TR_Type,'') as TR_Type From BLDTL Where BL_Number = '" & Trim(BL) & "' and seq_number = " & seq, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            If ds.Tables(0).Rows(0).Item("TR_Type") = "D" Then
                Return "DR"
            Else
                Return "BK"
            End If
        Else
            Return ""
        End If
    End Function

    Public Function GetDefaultPrinter() As String
        Dim settings As New System.Drawing.Printing.PrinterSettings
        Return settings.PrinterName
    End Function

    Public Function South_or_North_Bound(ByVal PortL As Integer) As DataSet
        Dim ds As New DataSet
        Dim ds_P As New DataSet
        If PortL > 0 Then
            ds = ws.GetDataset(md.strConnect, "Select isnull(Country,'') as Country From Ports Where Port_number = " & PortL, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                If Trim(ds.Tables(0).Rows(0).Item("Country")) = "USA" Or Trim(ds.Tables(0).Rows(0).Item("Country")) = "U.S.A." Or Trim(ds.Tables(0).Rows(0).Item("Country")) = "US" Then
                    md.strSQL = "SELECT Company_Name as Yard_Name, Company_Number as Yard_Number
                                                   From [dbo].[CM_System] 
                                                Where  (ISNULL(Port_Yard, '') = 'Y') and Country = 'USA' order by Company_Name"
                Else
                    md.strSQL = "SELECT Company_Name as Yard_Name, Company_Number as Yard_Number
                                                   From [dbo].[CM_System] 
                                                Where  (ISNULL(Port_Yard, '') = 'Y')  and Country <> 'USA' order by Company_Name"
                End If
                ds_P = ws.GetDataset(md.strConnect, md.strSQL, 1)
                If ds_P.Tables(0).Rows.Count > 0 Then

                End If
            End If
        Else
            md.strSQL = "SELECT Company_Name as Yard_Name, Company_Number as Yard_Number
                                                   From [dbo].[CM_System] 
                                                Where  (ISNULL(Port_Yard, '') = 'Y') Order by Company_Name"
            ds_P = ws.GetDataset(md.strConnect, md.strSQL, 1)
        End If

        Return ds_P
        ds_P = Nothing
        ds = Nothing
    End Function

    Public Function South_or_North(ByVal PortL As Integer) As String
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "Select isnull(Country,'') as Country From Ports Where Port_number = " & PortL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            If Trim(ds.Tables(0).Rows(0).Item("Country")) = "USA" Or Trim(ds.Tables(0).Rows(0).Item("Country")) = "U.S.A." Or Trim(ds.Tables(0).Rows(0).Item("Country")) = "US" Then
                Return "S"
            Else
                Return "N"
            End If
        End If
        ds = Nothing
    End Function

    Public Function Progran_access(ByVal Code As Integer, ByVal Program As String) As String
        strSQL = "Select Access FROM User_Module_Program WHERE UserCode = " & Code & " and (ltrim(rtrim(Program)) = '" & Trim(Program) & "')"
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return Trim(ds.Tables(0).Rows(0).Item("Access"))
        Else
            Return "N"
        End If
    End Function

    Public Function Progran_ReadOnly(ByVal Code As Integer, ByVal Program As String) As String
        strSQL = "Select ReadOnly FROM User_Module_Program WHERE UserCode = " & Code & " and (ltrim(rTrim(Program)) = '" & Trim(Program) & "')"
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return Trim(ds.Tables(0).Rows(0).Item("ReadOnly"))
        Else
            Return "N"
        End If
        ds = Nothing
    End Function

    Public Function Progran_is_Entry(ByVal ModuleA As String, ByVal Program As String) As String
        strSQL = "Select Entry FROM Programs_Master WHERE (ltrim(rtrim(Module)) = '" & Trim(ModuleA) & "') and (ltrim(rtrim(Program)) = '" & Trim(Program) & "') and (Isnull(Entry,'N') = 'Y')"
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return "Y"
        Else
            Return "N"
        End If
    End Function

    Public Function Program_Favorite(ByVal User_Code As Integer, ByVal Program_Name As String)
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim ds As New DataSet
        strSQL = "Select Distinct isNull(u.Favorite,'') as Favorite, isnull(d.Menu_Desc,'') as Menu_Desc  From User_Module_Program u Inner Join Module_Programs as d on d.Program = u.Program Where u.UserCode = " & UserCode & " and u.Program = '" & Trim(Program_Name) & "'"
        ds = ws.GetDataset(strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            If Trim(ds.Tables(0).Rows(0).Item("Favorite")) = "Y" Then
                strSQL = "Update User_Module_Program Set Favorite = NULL, MenuItem = NULL Where UserCode = " & User_Code & " and Program = '" & Trim(Program_Name) & "'"
            Else
                strSQL = "Update User_Module_Program Set Favorite = 'Y', MenuItem = '" & Trim(ds.Tables(0).Rows(0).Item("Menu_Desc")) & "'  Where UserCode = " & User_Code & " and Program = '" & Trim(Program_Name) & "'"
            End If
            eResp = ws.ExecSQL(strConnect, strSQL)
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function


    Public Function Sailing_Next(ByVal Sailing As String) As String
        Dim Portl As Integer = 0
        Dim Departure As Date = System.DateTime.Today.ToShortDateString
        Dim nRoute As Integer = 0
        Dim ds As New DataSet
        strSQL = "SELECT Route_ID,Port,Format(est_Departure,'yyyy-MM-dd') as Departure FROM dbo.Sailing_Master WHERE Sailing_nro = '" & Trim(Sailing) & "' and leg_Nro = 1"
        ds = GetDataset(strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Portl = ds.Tables(0).Rows(0).Item("Port")
            Departure = ds.Tables(0).Rows(0).Item("Departure")
            nRoute = ds.Tables(0).Rows(0).Item("Route_ID")
            strSQL = "Select TOP(1) Sailing_nro as Sailing From dbo.Sailing_Master
            Where (PORT = " & Portl & ") And (EST_DEPARTURE > '" & Departure & "') AND (Route_ID = " & nRoute & ") AND (Leg_Nro = 1)
            ORDER BY Sailing_nro"
            ds.Clear()
            ds = GetDataset(strConnect, strSQL, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                Return Trim(ds.Tables(0).Rows(0).Item("Sailing"))
            Else
                Return ""
            End If
        Else
            Return ""
        End If
    End Function

    Public Function Est_Arrive(ByVal Sailing As String, ByVal PortD As Integer) As Date
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, "SELECT Format(EST_DEPARTURE,'MM/dd/yyyy') as Est_arrive FROM Sailing_Master WHERE (Sailing_nro = '" & Sailing & "') AND (PORT = " & PortD & ") ORDER BY Leg_Nro DESC", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Est_Arrive")
        Else
            Return System.DateTime.Today.ToShortDateString
        End If
        ds = Nothing
    End Function

    Public Function Est_Departure(ByVal Sailing As String, ByVal PortL As Integer) As Date
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, "SELECT Top(1) Format(EST_DEPARTURE,'MM/dd/yyyy') as Est_Departure FROM Sailing_Master WHERE (Sailing_nro = '" & Sailing & "') AND (PORT = " & PortL & ") ORDER BY Leg_Nro", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Est_Departure")
        Else
            Return System.DateTime.Today.ToShortDateString
        End If
        ds = Nothing
    End Function

    Public Function Booking_Rolloved(ByVal Sailing As String, ByVal BK As Integer, ByVal TIR As Integer, ByVal yard As Integer) As Integer
        Dim vSailing As String = md.Sailing_Next(Sailing)
        Dim ds As New DataSet

        strSQL = "SELECT ACTIVITY_CODE,seq_Number FROM 
                    (SELECT BOOKING_NUMBER, seq_number, CONTAINERS, CONTAINER_TYPE, DESCRIPTION, MOV_TYPE, DC_Flag, Yard, TIR, BL_Number, BL_Seq,
                       (SELECT TOP (1) ACTIVITY_CODE FROM Eq_Movements
                               WHERE (Eq_Movements.STEVEDORE_NO = bk.Yard) AND (Eq_Movements.TIR_NUMBER = bk.TIR) ORDER BY uid DESC) AS ACTIVITY_CODE
                        FROM Booking_Detail AS bk
                       WHERE (BOOKING_NUMBER = " & BK & ") AND (ISNULL(TIR, 0) = " & TIR & ") AND (CONTAINER_TYPE <> 'LCL')) AS T1
                 WHERE (ACTIVITY_CODE <> 'LODF') AND (ACTIVITY_CODE <> 'DCHF') AND (ACTIVITY_CODE <> 'LODE') AND (ACTIVITY_CODE <> 'DCHE')"
        ds = ws.GetDataset(strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            If BK_Count_Lines(BK) = ds.Tables(0).Rows.Count Then
                ' ------- n Line = n Rows => all containers of the Booking must be moved to the next Sailing ---------
                ' ------- Simple, Booking is going to change Sailing #. Bookings_Headline.Sailing_nro = next Sailing -------------------------
                eResp = ws.ExecSQL(strConnect, "Update Bookings_Headline Set Sailing_nro = '" & Trim(vSailing) & "' Where Booking_Number = " & BK)
                ' ------- Update TIR_Cnt_Transfer, Eq_Movements ??????????????????
            Else
                Dim nBK As Integer = 0
                nBK = md.BK_Last + 1
                ' ------- There are some Containers must be moved to new Booking and Sailing ------------------------------------
                strSQL = "Insert Into Bookings_Headline ( BOOKING_NUMBER, BOOKING_DATE, TIME_STAMP, AMPM, MADE_BY, CONT_REQ, Voyage, Sailing_nro, PORT_DISCHARGE, PORT_LOADING, TRUCKER_NUMBER, TRUCKER_LOC, STEVEDORE_NO, 
                         SHIPPER_NUMBER, SHIPPER_NAME, SHIPPER_LOC, SHIPPER_REFS, SHIPPER_DESC, FWDR_NUMBER, FWDR_NAME, FWDR_LOC, FWDR_REFS, FWDR_DESC, FMC, CONTACT_NAME, CONSIGNEE_NO, CONSIGNEE_NAME, 
                         CONSIGNEE_LOC, CONS_DESC, NOTIFY_NO, NOTIFY_NAME, NOTIFY_LOC, NOTIFY_DESC, CONTACT_PHONE, BILL_TO, SPOT_TO, PORT_TRANSH, PORT_ORIGIN, SERV_CONTRACT, Notes, SHIPPER_EXTRA, FWDR_EXTRA, 
                         NOTIFY_EXTRA, CONS_EXTRA, BL_Number, TIR_Number, Created_ON, EDI_301, Empty_Reposition, FileNameSTR, EDI_301_IN)
                        SELECT " & nBK & ", BOOKING_DATE, TIME_STAMP, AMPM, MADE_BY, CONT_REQ, Voyage, " & Trim(vSailing) & ", PORT_DISCHARGE, PORT_LOADING, TRUCKER_NUMBER, TRUCKER_LOC, STEVEDORE_NO, 
                               SHIPPER_NUMBER, SHIPPER_NAME, SHIPPER_LOC, SHIPPER_REFS, SHIPPER_DESC, FWDR_NUMBER, FWDR_NAME, FWDR_LOC, FWDR_REFS, FWDR_DESC, FMC, CONTACT_NAME, CONSIGNEE_NO, CONSIGNEE_NAME, 
                               CONSIGNEE_LOC, CONS_DESC, NOTIFY_NO, NOTIFY_NAME, NOTIFY_LOC, NOTIFY_DESC, CONTACT_PHONE, BILL_TO, SPOT_TO, PORT_TRANSH, PORT_ORIGIN, SERV_CONTRACT, Notes, SHIPPER_EXTRA, FWDR_EXTRA, 
                               NOTIFY_EXTRA, CONS_EXTRA, BL_Number, TIR_Number, Created_ON, EDI_301, Empty_Reposition, FileNameSTR, EDI_301_IN
                        FROM Bookings_Headline
                        WHERE (BOOKING_NUMBER = " & BK & ")"
                eResp = ws.ExecSQL(strConnect, strSQL)

                strSQL = "Update Booking_Detail Set Booking_Number = " & nBK & ", Seq_Number = 1, Line_ID = 1 Where Booking_Number = " & BK & " and seq_Number = " & ds.Tables(0).Rows(0).Item("seq_number")
                eResp = ws.ExecSQL(strConnect, strSQL)
                strSQL = "Update TIR_Cnt_Transfer Set Sailing_nro = '" & Trim(vSailing) & "', Booking_Number = " & nBK & ", Booking_Seq = 1, Voyage = 'Rollover' Where (BOOKING_NUMBER = " & BK & ") AND (BOOKING_SEQ = " & ds.Tables(0).Rows(0).Item("seq_number") & ")"
                eResp = ws.ExecSQL(strConnect, strSQL)
                strSQL = "Update Eq_Movements Set Sailing_nro = '" & Trim(vSailing) & "', Note = 'Rollover' Where (TIR_NUMBER = " & TIR & ") AND (STEVEDORE_NO = " & yard & ")"
                eResp = ws.ExecSQL(strConnect, strSQL)
                Return nBK
            End If

        Else

        End If
    End Function

    Public Sub TIR_Change_Container(ByVal Yard As Integer, ByVal TIR As Integer, ByVal Cont1 As String, ByVal Cont2 As Integer, ByVal Cont3 As String)
        Dim ds As New DataSet
        ' ------- looking for Container_ID and Container ---------------------
        strSQL = "Select Container_ID,Container, Equipment_Type From Equipment_Master Where Container_No_1 = '" & Cont1 & "' and Container_no_2 = " & Cont2 & " and Container_no_3 = '" & Trim(Cont3) & "'"
        Dim new_Cont_ID As Integer = 0
        Dim new_Container As String = ""
        Dim new_Eq As String = ""
        ds = ws.GetDataset(strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            new_Cont_ID = ds.Tables(0).Rows(0).Item("Container_ID")
            new_Container = Trim(ds.Tables(0).Rows(0).Item("Container"))
            new_Eq = Trim(ds.Tables(0).Rows(0).Item("Equipment_Type"))
        End If
        ' ------------------------------------------------------------------------
        ds.Clear()
        strSQL = "Select Sailing_nro, Booking_Number, Booking_Seq, Container, isnull(Cont_1,'') as Cont1, isnull(Cont_2,0) as Cont2, isnull(Cont_3,'') as Cont3 From TIR_Cnt_Transfer Where DR_Stevedoring = " & Yard & " and DR_Number = " & TIR
        ds = ws.GetDataset(strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            'If md.Ask_Equipment_is_Container Then
            ' ------- Update Eq_Master (History) --------------------------------------------------------------------------------------------------------------------
            strSQL = "Update Equipment_Master Set Container_ID = " & new_Cont_ID & ", Container = '" & Trim(new_Container) &
                                    "', Container_no_1 = '" & Trim(Cont1) & "', Container_no_2 = " & Cont2 & ", Container_no_3 = '" & Trim(Cont3) &
                        "', Equipment_Type = '" & Trim(new_Eq) & "' Where Stevedore_no = " & Yard & " and TIR_Number = " & TIR & " and Container = '" & Trim(ds.Tables(0).Rows(0).Item("Container")) & "'"


            ' ------- Update Eq_Movements (History) --------------------------------------------------------------------------------------------------------------------
            strSQL = "Update Eq_Movements Set Container_ID = " & new_Cont_ID & ", Container = '" & Trim(new_Container) &
                                "', Container_no_1 = '" & Trim(Cont1) & "', Container_no_2 = " & Cont2 & ", Container_no_3 = '" & Trim(Cont3) &
                    "', Equipment_Type = '" & Trim(new_Eq) & "' Where Stevedore_no = " & Yard & " and TIR_Number = " & TIR & " and Container = '" & Trim(ds.Tables(0).Rows(0).Item("Container")) & "'"
            eResp = ws.ExecSQL(strConnect, strSQL)
            ' ------- Update TIR  --------------------------------------------------------------------------------------------------------------------
            strSQL = "Update TIR_Cnt_Transfer Set Container = '" & Trim(new_Container) &
                                "', Cont_1 = '" & Trim(Cont1) & "', Cont_2 = " & Cont2 & ", Cont_3 = '" & Trim(Cont3) &
                    "', Equipment_Type = '" & Trim(new_Eq) & "' Where DR_STEVEDORING = " & Yard & " and DR_Number = " & TIR & " and Container = '" & Trim(ds.Tables(0).Rows(0).Item("Container")) & "'"
            eResp = ws.ExecSQL(strConnect, strSQL)

        End If
    End Sub

    Function Send_File_via_https(ByVal url As String, ByVal strFilename As String) As String
        Dim cmd As String = Trim(url)
        '"https://secure.amazon.com/exec/panama/seller-admin/catalog-upload/add-modify-delete"
        '"https://wwwtest1.myvan.descartes.com/HttpUpload/SimpleUploadUI.aspx"
        '"https://wwwtest1.myvan.descartes.com/HttpUpload/SimpleUploadUI.aspx"

        Dim fInfo As System.IO.FileInfo
        Dim fBytes() As Byte
        Dim fLen As Long
        Dim fStream As System.IO.FileStream
        Dim fName As String = strFilename

        Dim webReq As System.Net.HttpWebRequest = CType(System.Net.WebRequest.Create(cmd), System.Net.HttpWebRequest)
        Dim reqStream As System.IO.Stream
        Dim userInfo As String = "ldelanuez:AyGk6H=f" '"phatcampus@hotmail.com:sterling818"
        'Const cookie As String = " x-main=YvjPkwfntqDKun0QEmVRPcTTZDMe?Tn?;ubid-main=002-8989859-9917520;ubid-tacbus=019-5423258-4241018;x-tacbus=vtm4d53DvX@Sc9LxTnAnxsFL3DorwxJa ; ubid-tcmacb=087-805Stre5947-0795529;ubid-ty2kacbus=161-5477122-2773524; session-id=087-178254-5924832; session-id-time=950660664"

        'get the size of the file and size the array 
        fInfo = New System.IO.FileInfo(fName)
        fLen = fInfo.Length
        If fLen > 0 Then
            ReDim fBytes(fLen)
        End If

        'read the file into the array 
        fStream = System.IO.File.OpenRead(fName)
        fStream.Read(fBytes, 0, fLen)
        fStream.Close()

        'set up the Request headers 
        With webReq
            .KeepAlive = False
            .ContentLength = 0
            .ContentLength = fBytes.Length
            .ContentType = "text/xml"
            .Method = "POST"

            .Headers.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(userInfo)))
            '.Headers.Add("Cookie", cookie)
            '.Headers.Add("dev-t", "1Q60X1A1BSK5FX634J02")
            '.Headers.Add("UploadFor", "Marketplace")
            .Headers.Add("FileFormat", "TabDelimited")
        End With

        'open the stream and write the array to it, then do cleanup 
        reqStream = webReq.GetRequestStream()
        With reqStream
            .Write(fBytes, 0, fBytes.Length)
            .Flush()
            .Close()
        End With
        reqStream = Nothing

        'get the Response and display to user 
        Dim webResp As System.Net.HttpWebResponse = CType(webReq.GetResponse(), System.Net.HttpWebResponse)
        Dim respStream As System.IO.Stream = webResp.GetResponseStream()

        Dim stReader As New System.IO.StreamReader(respStream)
        Dim respStr As String = stReader.ReadToEnd()
        Return respStr
    End Function
    Function OceanV1_Send_File_via_https(ByVal url As String, ByVal strFilename As String, ByVal strFileNameResponse As String) As String
        Dim fileReader As String
        fileReader = My.Computer.FileSystem.ReadAllText(strFilename)

        Dim xmlHttp As New MSXML2.ServerXMLHTTP60
        xmlHttp.setOption(2, 13056)
        xmlHttp.setTimeouts(1000, 1000, 1000, 500000)
        'Inventory "https://www.myvan.descartes.com/HttpUpload/SimpleUploadHandler.aspx"
        xmlHttp.open("POST", url, False)
        Try
            Dim csUserNamePassword As String
            csUserNamePassword = "ldelanuez:NZ5yCPM="
            xmlHttp.setRequestHeader("Authorization", "Basic " + Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(csUserNamePassword)))
            xmlHttp.setRequestHeader("Content-Type", "text/xml")
            xmlHttp.send(fileReader)
            'Get the server's response 
            Dim sResponse As String = xmlHttp.responseText

            sResponse = Trim(sResponse)
            If xmlHttp.status <> 200 Then
                Return "error with server. HTTP status code : " & xmlHttp.status & " Text : " & xmlHttp.statusText
            End If
            sResponse = Trim(sResponse)
            Dim nro As Integer = FreeFile()
            '------- Delete File of out ---------------------
            If (System.IO.File.Exists(strFileNameResponse)) Then
                System.IO.File.Delete(strFileNameResponse)
            End If
            '------- Open File of out -----------------
            FileOpen(nro, strFileNameResponse, OpenMode.Binary)
            FilePut(nro, sResponse)
            'strResp)
            FileClose(nro)
            Return "Success"
        Catch ex As Exception
            'MsgBox(ex.Message.ToString)
            Return ex.Message.ToString
        End Try
    End Function
    Function OceanACE_355_response()
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            Dim vCount As Integer = 0
            Dim vBL As String = ""
            Dim vAccepted As String = ""

            Dim dsExists As New DataSet
            Dim ds As New DataSet
            ds.ReadXml("C:\EDI\OceanACEXML\XML_Files\OceanACE_Response.xml")

            Dim j As Integer = 0
            For j = 0 To ds.Tables("billOfLading").Rows.Count - 1
                vBL = ds.Tables("billOfLading").Rows(j).Item("billOfLadingNumber")
                vAccepted = ds.Tables("billOfLading").Rows(j).Item("applicationStatus")

                dsExists.Clear()
                strSQL = "SELECT * FROM BillOfLoadings Where BL_Number = '" & Trim(vBL) & "'"
                dsExists = ws.GetDataset(strConnect, strSQL, 1)
                If dsExists.Tables(0).Rows.Count > 0 Then
                    strSQL = "Insert into BL_OceanACe_Response 
                            (BL_Number, Action, receivedDateTime, Container,
                              customsStatus, applicationStatus, dispositionCode,dispositionStatus, Create_On
                             ) Values ('" &
                            Trim(vBL) & "','355','" & ds.Tables("billOfLading").Rows(j).Item("receivedDateTime") & "','','" &
                            Trim(ds.Tables("billOfLading").Rows(j).Item("customsStatus")) & "','" & Trim(ds.Tables("billOfLading").Rows(j).Item("applicationStatus")) & "','','" &
                            Trim(ds.Tables("billOfLading").Rows(j).Item("dispositionStatus")) & "','" & System.DateTime.Now & "')"
                    ExecSQL(strConnect, strSQL)
                Else
                    MsgBox("BL number: " & Trim(vBL) & " not found,...")
                End If
            Next
            dsExists = Nothing
            ds = Nothing
        Catch e As Exception
            MsgBox(e.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Function Seal_15(ByVal Seal As String) As String
        If Len(Trim(Seal)) > 0 Then
            If Len(Trim(Seal)) < 15 Then
                Dim pos As Integer = 15 - Seal.Length
                Dim vSTR As String = ""
                vSTR = New String("0", pos)
                Seal = Trim(Seal) & Trim(vSTR)
                Return Seal
            End If
        Else
            Return "KOSL01234567"
        End If
    End Function

    Function P_and_L_last_5_Year_x_Country(ByVal cmp As Integer, ByVal Country As String, ByVal pYear As Integer, ByVal PortD As Integer) As DataSet
        Dim ds As New DataSet
        strSQL = "Select pYear as Period_Year,1 as Period_Month, 'January' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear) AND (a.PERIOD_MONTH = 1) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION 
	       Select pYear as Period_Year,2 as Period_Month, 'Februery' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear) AND (a.PERIOD_MONTH = 2) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION 
	    Select pYear as Period_Year,3 as Period_Month, 'March' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear) AND (a.PERIOD_MONTH = 3) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION 
        Select pYear as Period_Year,4 as Period_Month, 'April' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear) AND (a.PERIOD_MONTH = 4) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION 
        Select pYear as Period_Year,5 as Period_Month, 'May' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear) AND (a.PERIOD_MONTH = 5) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION 
      Select pYear as Period_Year,6 as Period_Month, 'June' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear) AND (a.PERIOD_MONTH = 6) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION    
	 Select pYear as Period_Year,7 as Period_Month, 'July' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear) AND (a.PERIOD_MONTH = 7) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION    
	Select pYear as Period_Year,8 as Period_Month, 'August' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear) AND (a.PERIOD_MONTH = 8) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION    
   
   Select pYear as Period_Year,9 as Period_Month, 'September' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear) AND (a.PERIOD_MONTH = 9) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION                
	Select pYear as Period_Year,10 as Period_Month, 'October' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear) AND (a.PERIOD_MONTH = 10) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
	Union
	      Select pYear as Period_Year,11 as Period_Month, 'November' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear) AND (a.PERIOD_MONTH = 11) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION 
          Select pYear as Period_Year,12 as Period_Month, 'December' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear) AND (a.PERIOD_MONTH = 12) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2

UNION

       Select pYear - 1 as Period_Year,1 as Period_Month, 'January' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 1) AND (a.PERIOD_MONTH = 1) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION 
	       Select pYear - 1 as Period_Year,2 as Period_Month, 'Februery' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 1) AND (a.PERIOD_MONTH = 2) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION 
	    Select pYear - 1 as Period_Year,3 as Period_Month, 'March' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 1) AND (a.PERIOD_MONTH = 3) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION 
        Select pYear - 1 as Period_Year,4 as Period_Month, 'April' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 1) AND (a.PERIOD_MONTH = 4) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION 
        Select pYear - 1 as Period_Year,5 as Period_Month, 'May' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 1) AND (a.PERIOD_MONTH = 5) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION 
      Select pYear - 1 as Period_Year,6 as Period_Month, 'June' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 1) AND (a.PERIOD_MONTH = 6) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION    
	 Select pYear - 1 as Period_Year,7 as Period_Month, 'July' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 1) AND (a.PERIOD_MONTH = 7) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION    
	Select pYear - 1 as Period_Year,8 as Period_Month, 'August' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 1) AND (a.PERIOD_MONTH = 8) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION    
   
   Select pYear - 1 as Period_Year,9 as Period_Month, 'September' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 1) AND (a.PERIOD_MONTH = 9) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION                
	Select pYear - 1 as Period_Year,10 as Period_Month, 'October' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 1) AND (a.PERIOD_MONTH = 10) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
	Union
	      Select pYear - 1 as Period_Year,11 as Period_Month, 'November' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 1) AND (a.PERIOD_MONTH = 11) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION 
          Select pYear - 1 as Period_Year,12 as Period_Month, 'December' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 1) AND (a.PERIOD_MONTH = 12) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2

union

Select pYear - 2 as Period_Year,1 as Period_Month, 'January' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 2) AND (a.PERIOD_MONTH = 1) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION 
	       Select pYear - 2 as Period_Year,2 as Period_Month, 'Februery' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 2) AND (a.PERIOD_MONTH = 2) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION 
	    Select pYear - 2 as Period_Year,3 as Period_Month, 'March' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 2) AND (a.PERIOD_MONTH = 3) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION 
        Select pYear - 2 as Period_Year,4 as Period_Month, 'April' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 2) AND (a.PERIOD_MONTH = 4) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION 
        Select pYear - 2 as Period_Year,5 as Period_Month, 'May' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 2) AND (a.PERIOD_MONTH = 5) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION 
      Select pYear - 2 as Period_Year,6 as Period_Month, 'June' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 2) AND (a.PERIOD_MONTH = 6) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION    
	 Select pYear - 2 as Period_Year,7 as Period_Month, 'July' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 2) AND (a.PERIOD_MONTH = 7) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION    
	Select pYear - 2 as Period_Year,8 as Period_Month, 'August' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 2) AND (a.PERIOD_MONTH = 8) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION    
   
   Select pYear - 2 as Period_Year,9 as Period_Month, 'September' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 2) AND (a.PERIOD_MONTH = 9) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION                
	Select pYear - 2 as Period_Year,10 as Period_Month, 'October' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 2) AND (a.PERIOD_MONTH = 10) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
	Union
	      Select pYear - 2 as Period_Year,11 as Period_Month, 'November' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 2) AND (a.PERIOD_MONTH = 11) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION 
          Select pYear - 2 as Period_Year,12 as Period_Month, 'December' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 2) AND (a.PERIOD_MONTH = 12) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2

Union
      Select pYear - 3 as Period_Year,1 as Period_Month, 'January' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 3) AND (a.PERIOD_MONTH = 1) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION 
	       Select pYear - 3 as Period_Year,2 as Period_Month, 'Februery' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 3) AND (a.PERIOD_MONTH = 2) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION 
	    Select pYear - 3 as Period_Year,3 as Period_Month, 'March' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 3) AND (a.PERIOD_MONTH = 3) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION 
        Select pYear - 3 as Period_Year,4 as Period_Month, 'April' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 3) AND (a.PERIOD_MONTH = 4) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION 
        Select pYear - 3 as Period_Year,5 as Period_Month, 'May' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 3) AND (a.PERIOD_MONTH = 5) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION 
      Select pYear - 3 as Period_Year,6 as Period_Month, 'June' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 3) AND (a.PERIOD_MONTH = 6) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION    
	 Select pYear - 3 as Period_Year,7 as Period_Month, 'July' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 3) AND (a.PERIOD_MONTH = 7) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION    
	Select pYear - 3 as Period_Year,8 as Period_Month, 'August' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 3) AND (a.PERIOD_MONTH = 8) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION    
   
   Select pYear - 3 as Period_Year,9 as Period_Month, 'September' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 3) AND (a.PERIOD_MONTH = 9) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION                
	Select pYear - 3 as Period_Year,10 as Period_Month, 'October' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 3) AND (a.PERIOD_MONTH = 10) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
	Union
	      Select pYear - 3 as Period_Year,11 as Period_Month, 'November' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 3) AND (a.PERIOD_MONTH = 11) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION 
          Select pYear - 3 as Period_Year,12 as Period_Month, 'December' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 3) AND (a.PERIOD_MONTH = 12) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2

Union
      Select pYear - 4 as Period_Year,1 as Period_Month, 'January' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 4) AND (a.PERIOD_MONTH = 1) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION 
	       Select pYear - 4 as Period_Year,2 as Period_Month, 'Februery' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 4) AND (a.PERIOD_MONTH = 2) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION 
	    Select pYear - 4 as Period_Year,3 as Period_Month, 'March' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 4) AND (a.PERIOD_MONTH = 3) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION 
        Select pYear - 4 as Period_Year,4 as Period_Month, 'April' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 4) AND (a.PERIOD_MONTH = 4) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION 
        Select pYear - 4 as Period_Year,5 as Period_Month, 'May' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 4) AND (a.PERIOD_MONTH = 5) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION 
      Select pYear - 4 as Period_Year,6 as Period_Month, 'June' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 4) AND (a.PERIOD_MONTH = 6) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION    
	 Select pYear - 4 as Period_Year,7 as Period_Month, 'July' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 4) AND (a.PERIOD_MONTH = 7) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION    
	Select pYear - 4 as Period_Year,8 as Period_Month, 'August' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 4) AND (a.PERIOD_MONTH = 8) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION    
   
   Select pYear - 4 as Period_Year,9 as Period_Month, 'September' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 4) AND (a.PERIOD_MONTH = 9) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION                
	Select pYear - 4 as Period_Year,10 as Period_Month, 'October' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 4) AND (a.PERIOD_MONTH = 10) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
	Union
	      Select pYear - 4 as Period_Year,11 as Period_Month, 'November' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 4) AND (a.PERIOD_MONTH = 11) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2
   
   UNION 
          Select pYear - 4 as Period_Year,12 as Period_Month, 'December' as P_M_name, isnull(Sum(Amount),0) as Amount From
				   (SELECT  Port_Country, PORTL_NUMBER, PortL, rtrim(PortL_Name) + ' (' + rtrim(Portl) + ')' as PortL_Name, PORTD_NUMBER, PortD, PORTT_NUMBER, PortT, ACCOUNT, RTRIM(ACCOUNT) + ' - ' + RTRIM(DESC_ENG) AS Desc_ENG, 
                                 Case account 
			                      when '4295' then (Amount) * (-1)
				                  else Amount
			                   end as Amount
                    FROM (SELECT pl.COUNTRY AS Port_Country, a.PORTL_NUMBER, pl.PORT_SHORT AS PortL, pl.PORT_NAME AS PortL_Name, a.PORTD_NUMBER, pd.PORT_SHORT AS PortD, a.PORTT_NUMBER, pf.PORT_SHORT AS PortT, 
                                 a.ACCOUNT, c.DESC_ENG, SUM(a.AMOUNT) AS Amount
                          FROM dbo.ARIDT_GDZ AS a INNER JOIN
                               dbo.GLACCT_GDZ AS c ON c.Company_Number = cmp AND c.ACCOUNT = a.ACCOUNT INNER JOIN
                               dbo.Ports AS pl ON pl.PORT_NUMBER = a.PORTL_NUMBER INNER JOIN
                               dbo.Ports AS pd ON pd.PORT_NUMBER = a.PORTD_NUMBER INNER JOIN
                               dbo.Ports AS pf ON pf.PORT_NUMBER = a.PORTT_NUMBER
                          WHERE (a.Company_Number = cmp) AND (a.PERIOD_YEAR = pYear - 4) AND (a.PERIOD_MONTH = 12) and RTRIM(pl.Country) = 'Country'
                          GROUP BY pl.COUNTRY, a.PORTL_NUMBER, pl.PORT_SHORT, pl.PORT_NAME, a.PORTD_NUMBER, pd.PORT_SHORT, a.PORTT_NUMBER, pf.PORT_SHORT, a.ACCOUNT, c.DESC_ENG) AS T1
                  WHERE (SUBSTRING(ACCOUNT, 1, 1) = '4') AND (PortD_Number = PortD)) as T2


"
        ds = ws.GetDataset(strConnect, strSQL, 1)
        Return ds
    End Function

#Region "Quotation_Breakbck"
    Public Function DC_BY_Quote(ByVal Quote As Integer) As DataSet
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT Quote_Number, isnull(Quote_Seq,991) as Quote_seq,isnull(Unit_Nro,1) as Unit_Nro, SEQ_NUMBER as Line_ID, UN_NUMBER as UN_NUM, Class_Number, 
       DC_DESC as Proper_name,Class_Label,FlashPoint,PAGE_NUMBER,PACKAGING_GROUP,FP_UNIT,SUBRISK, Pkgs, PKG_TYPE as  Pkg_Unit, 
                         Weight, Weight_Unit, Contact, Created_By, Created_On
                         FROM QuoteDC 
                         WHERE (Quote_Number =" & Quote & ") and (Manual_Flag='N') Order By isnull(Quote_Seq,991),isnull(Unit_Nro,0),SEQ_NUMBER", 1)
        Return ds
    End Function

    Public Function Quote_Last() As Integer
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "SELECT TOP (1) Quote_NUMBER FROM QuoteHDR ORDER BY Quote_NUMBER DESC", 1)
            If ds.Tables(0).Rows.Count > 0 Then
                Return ds.Tables(0).Rows(0).Item("Quote_Number")
            Else
                Return 1
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Public Function Quote_First() As Integer
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "SELECT TOP (1) Quote_NUMBER FROM QuoteHDR ORDER BY Quote_NUMBER", 1)
            If ds.Tables(0).Rows.Count > 0 Then
                Return ds.Tables(0).Rows(0).Item("Quote_Number")
            Else
                Return 1
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Public Function Quote_Audit_BY_Quote(ByVal Quote As Integer) As DataSet
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT Quote_Number, Description, Created_ON, Created_By, Old_value FROM QuoteJournal
                         WHERE  (Quote_Number =" & Quote & ") Order By uid desc", 1)
        Return ds
    End Function
    Public Function Quote_x_BK(ByVal BK As Integer) As String
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "SELECT TOP (1) Quote_NUMBER FROM QuoteHDR Where Booking_Number = " & BK, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                Return Trim(Str(ds.Tables(0).Rows(0).Item("Quote_Number")))
            Else
                Return ""
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Public Function Quote_Number() As Integer
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Try
            Dim ds As New DataSet
            ds = ws.GetDataset(md.strConnect, "SELECT TOP (1) uid FROM Quotations ORDER BY uid DESC", 1)
            If ds.Tables(0).Rows.Count > 0 Then
                Return ds.Tables(0).Rows(0).Item("uid")
            Else
                Return 0
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

#End Region

    Public Function FWDR_Get_FMC(ByVal FWDR_Acc As Integer) As String
        Dim FMC As String = ""
        Dim strSQL As String = "SELECT isnull(FMC,'') as FMC FROM  dbo.CM_System WHERE (COMPANY_Number = " & FWDR_Acc & ")"
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            FMC = Trim(ds.Tables(0).Rows(0).Item("FMC"))
        End If
        ds = Nothing
        strSQL = Nothing
        Return Trim(FMC)
    End Function

    Public Function Get_Param_Char_Add(ByVal Line As String, ByVal Param As Integer) As String
        Line = md.RemoveSpaces(Line)
        Dim j As Integer = 0
        Dim pos1 As Integer = 0
        Dim pos2 As Integer = 0
        Dim pos As Integer = 0
        Dim vP As String = ""
        pos1 = InStr(Line, "+")
        pos = pos1
        Select Case Param
            Case 1
                pos2 = InStr(pos1 + 1, Line, "+")
                vP = Mid(Line, pos1 + 1, pos2 - (pos1 + 1))
            Case > 1
                For j = 1 To Param
                    pos2 = InStr(pos + 1, Line, "+")
                    'If pos2 > pos + 1 Then
                    If pos2 = 0 Then
                        pos2 = InStr(pos + 1, Line, "'")
                    End If
                    vP = Mid(Line, pos + 1, pos2 - (pos + 1))
                    pos = pos2
                    'Else
                    '    pos = pos + 1
                    'End If
                Next
        End Select
        Return Trim(vP)
    End Function

    Public Function Get_Param_Chars_ZZZ(ByVal Line As String, ByVal Param As Integer) As String
        If Len(Trim(Line)) > 0 Then
            Dim j As Integer = 0
            Dim pos1 As Integer = 0
            Dim pos2 As Integer = 0
            Dim pos As Integer = 0
            Dim vP As String = ""
            pos1 = InStr(Line, "ZZZ")
            pos = pos1
            Select Case Param
                Case 1
                    pos2 = InStr(pos1 + 3, Line, "ZZZ")
                    vP = Mid(Line, pos1 + 3, pos2 - (pos1 + 3))
                Case > 1
                    For j = 1 To Param
                        pos2 = InStr(pos + 3, Line, "ZZZ")
                        'If pos2 > pos + 1 Then
                        If pos2 = 0 Then
                            pos2 = InStr(pos + 3, Line, "'")
                        End If
                        vP = Mid(Line, pos + 3, pos2 - (pos + 3))
                        pos = pos2
                        'Else
                        '    pos = pos + 1
                        'End If
                    Next
            End Select
            Return Trim(vP)
        Else
            Return ""
        End If
    End Function

#Region "Cash REceipt"
    Public Function CR_Get_Check(ByVal nCR As Integer, ByVal nYear As Integer, ByVal nMonth As Integer, ByVal Invoice As Integer) As String
        Dim strSQL As String = "SELECT Check FROM CRDtl WHERE CASH_RECEIPT = " & nCR & " and Period_Year = " & nYear & " and Period_Month = " & nMonth & " and Invoice = " & Invoice
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return Trim(ds.Tables(0).Rows(0).Item("Check"))
            ds = Nothing
            strSQL = Nothing
        Else
            Return ""
            ds = Nothing
            strSQL = Nothing
        End If
    End Function
    Public Function CR_Get_Posted(ByVal nCR As Integer, ByVal nYear As Integer, ByVal nMonth As Integer) As Boolean
        strSQL = "SELECT Top 1 isnull(Posted,'') as Posted FROM CRHDR WHERE CASH_RECEIPT = " & nCR & " and Period_Year = " & nYear & " and Period_Month = " & nMonth
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            If Trim(ds.Tables(0).Rows(0).Item("Posted")) = "P" Then
                Return True
                ds = Nothing
            Else
                Return False
                ds = Nothing
            End If
        Else
            Return False
            ds = Nothing
        End If
    End Function

    Public Function CR_Get_Seq(ByVal nCR As Integer, ByVal nYear As Integer, ByVal nMonth As Integer) As Integer
        strSQL = "SELECT Top 1 (Seq + 1) as Seq_number FROM CRDtl WHERE CASH_RECEIPT = " & nCR & " and Period_Year = " & nYear & " and Period_Month = " & nMonth & " Order By seq Desc "
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Seq_Number")
            ds = Nothing
        Else
            Return 1
            ds = Nothing
        End If
    End Function
#End Region

#Region "AR"
    Public Function AR_Get_Amount_Paid(ByVal Invoice As Integer) As Double
        strSQL = "SELECT SUM(AMOUNT) AS Amount FROM dbo.CRDTL WHERE (INVOICE = " & Invoice & ")"
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Amount")
            ds = Nothing
        Else
            Return 0.00
            ds = Nothing
        End If
    End Function

    ' ------- Change done on 09/30/2022, Get Charge From AR Account
    Public Function AR_Get_Charge_From_AR_Account(ByVal Account As String) As Integer
        strSQL = "SELECT Charge_Number FROM dbo.Cargos WHERE (AR_Acc = '" & Trim(Account) & "') Order By Charge_Number"
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Charge_Number")
            ds = Nothing
        Else
            Return 0
            ds = Nothing
        End If
    End Function
#End Region

#Region "AP"
    Public Function AP_Get_Amount_Paid(ByVal Invoice As Integer) As Double
        strSQL = "SELECT SUM(AMOUNT) AS Amount FROM dbo.CRDTL WHERE (INVOICE = " & Invoice & ")"
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Amount")
            ds = Nothing
        Else
            Return 0.00
            ds = Nothing
        End If
    End Function
#End Region

#Region "Booking / BL Print Notes"
    Public Function Pkg_Get_Booking_Notes(ByVal Av As String) As String
        Dim Note As String = ""
        strSQL = "SELECT ISNULL(Booking_Notes, '') AS Booking_Notes FROM PKGDESC Where (ABREVIATION = '" & Trim(Av) & "')"
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Note = Trim(ds.Tables(0).Rows(0).Item("Booking_Notes"))
        End If
        ds = Nothing
        Return Trim(Note)
    End Function
    Public Function Pkg_Get_Booking_Dtl_Notes(ByVal Booking As Integer) As String
        Dim Note As String = ""
        strSQL = "SELECT Distinct ISNULL(Type_of_Pkgs, '') AS Type_of_Pkgs FROM Booking_Detail Where (Booking_Number = " & Booking & ") Order By ISNULL(Type_of_Pkgs, '')"
        Dim j As Integer = 0
        Dim ds_BKs As New DataSet
        Dim ds As New DataSet
        ds_BKs = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds_BKs.Tables(0).Rows.Count > 0 Then
            For j = 0 To ds_BKs.Tables(0).Rows.Count - 1
                ds.Clear()
                strSQL = "SELECT ISNULL(Booking_Notes, '') AS Booking_Notes FROM PKGDESC Where (ABREVIATION = '" & Trim(ds_BKs.Tables(0).Rows(j).Item("Type_of_Pkgs")) & "')"
                ds = ws.GetDataset(md.strConnect, strSQL, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Len(Trim(Note)) = 0 Then
                        Note = Trim(ds.Tables(0).Rows(0).Item("Booking_Notes")) & vbCrLf
                    Else
                        Note = Note & Trim(ds.Tables(0).Rows(0).Item("Booking_Notes")) & vbCrLf
                    End If
                End If
            Next
        End If
        ds = Nothing
        Return Trim(Note)
    End Function


    Public Function Pkg_Get_Abreviation(ByVal vDesc As String) As String
        Dim Note As String = ""
        strSQL = "SELECT ISNULL(ABREVIATION, '') AS ABREVIATION FROM PKGDESC Where (Eng_Desc = '" & Trim(vDesc) & "')"
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Note = Trim(ds.Tables(0).Rows(0).Item("ABREVIATION"))
        End If
        ds = Nothing
        Return Trim(Note)
    End Function

    Public Function Notes_BY_BK(ByVal BK As Integer) As DataSet
        Dim ds As New DataSet
        strSQL = "Select Notes, isnull(Subject,'') as Subject,isnull(Created_By,'') as Created_By,Format(Created_Date,'MM/dd/yyyy') as Created_Date, ID from BKNotes where Booking_number = " & Trim(BK) & " Order By Created_Date desc"
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        Return ds
    End Function
    Public Function EqType_Get_Booking_Notes(ByVal Eq As String) As String
        Dim Note As String = ""
        strSQL = "SELECT ISNULL(Booking_Notes, '') AS Booking_Notes FROM EqpMts Where (Equipment_Type = '" & Trim(Eq) & "')"
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Note = Trim(ds.Tables(0).Rows(0).Item("Booking_Notes"))
        End If
        ds = Nothing
        Return Trim(Note)
    End Function
    Public Function EqType_Get_Booking_Dtl_Notes(ByVal Booking As Integer) As String
        Dim Note As String = ""
        strSQL = "SELECT Distinct ISNULL(Container_Type, '') AS Container_Type FROM Booking_Detail Where (Booking_Number = " & Booking & ") Order By ISNULL(Container_Type, '')"
        Dim j As Integer = 0
        Dim ds_BKs As New DataSet
        Dim ds As New DataSet
        ds_BKs = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds_BKs.Tables(0).Rows.Count > 0 Then
            For j = 0 To ds_BKs.Tables(0).Rows.Count - 1
                ds.Clear()
                strSQL = "SELECT ISNULL(Booking_Notes, '') AS Booking_Notes FROM EqpMts Where (Equipment_Type = '" & Trim(ds_BKs.Tables(0).Rows(j).Item("Container_Type")) & "')"
                ds = ws.GetDataset(md.strConnect, strSQL, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    If Len(Trim(Note)) = 0 Then
                        Note = Trim(ds.Tables(0).Rows(0).Item("Booking_Notes")) & vbCrLf
                    Else
                        Note = Note & Trim(ds.Tables(0).Rows(0).Item("Booking_Notes")) & vbCrLf
                    End If
                End If
            Next
        End If
        ds = Nothing
        Return Trim(Note)
    End Function

    Public Function Terminal_Get_Clause_PortL_PortD(ByVal Terminal_Load As Integer, Terminal_Discharge As Integer) As String
        Dim Note As String = ""
        strSQL = "SELECT Distinct ISNULL(Clause, '') AS Clause FROM BL_Terminal_Clauses Where (Terminal = " & Terminal_Load & ") and (Terminal_Dest = " & Terminal_Discharge & ")"
        Dim j As Integer = 0
        Dim ds_Note As New DataSet
        ds_Note = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds_Note.Tables(0).Rows.Count > 0 Then
            If Len(Trim(Note)) = 0 Then
                Note = Trim(ds_Note.Tables(0).Rows(0).Item("Clause")) & vbCrLf
            End If
        End If
        ds_Note = Nothing
        Return Trim(Note)
    End Function
#End Region


#Region "Commodities"
    Public Function Commdty_Get_Desc(ByVal ID As Integer) As String
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        strSQL = "SELECT ID, NAME FROM dbo.COMDTY WHERE ID = " & ID
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Name")
        Else
            Return ""
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Public Function Commdty_Get_ID(ByVal Name As String) As Integer
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        strSQL = "SELECT ID, NAME FROM dbo.COMDTY WHERE Name = '" & Trim(Name) & "'"
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("ID")
        Else
            Return 0
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
#End Region

#Region "Charges"
    Public Function Charge_Get_Desc(ByVal Charge_Number As Integer) As String
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        strSQL = "SELECT Charge_Number, Description FROM Cargos WHERE Charge_Number = " & Charge_Number
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Description")
        Else
            Return ""
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Public Function Charge_Get_Contract_Column(ByVal Charge_Number As Integer) As Integer
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        strSQL = "SELECT CHARGE_NUMBER, DESCRIPTION, Contract, Contract_Column FROM dbo.CARGOS WHERE Charge_Number = " & Charge_Number
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Contract_Column")
        Else
            Return 0
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Public Function Charge_Get_Contract_Charge_From_Column(ByVal nColumn As Integer) As Integer
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        strSQL = "SELECT   CHARGE_NUMBER, Contract, Contract_Column FROM CARGOS WHERE (ISNULL(Contract, 'N') = 'Y') and Contract_Column  = " & nColumn
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Charge_Number")
        Else
            Return 0
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
    Public Function Charge_Get_cargo_From_Desc(ByVal Desc As String) As Integer
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        strSQL = "SELECT Charge_Number, Description FROM Cargos WHERE Description = '" & Trim(Desc) & "'"
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Charge_Number")
        Else
            Return 0
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function
#End Region

    Public Function MachineName(ByVal vName As String) As Boolean
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        strSQL = "SELECT MachineName FROM Version_Machine WHERE ltrim(rtrim(MachineName)) = '" & Trim(vName) & "'"
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return False
        Else
            Return True
        End If
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    Public Function Rate_Minimum_LCL(ByVal PortD As Integer, ByVal PortT As Integer, ByVal nCF As Double, ByVal Rate As Double) As Double
        Dim nMin As Double = 0.00
        Dim strSQL As String = "SELECT  CFT_0_5, CFT_6_10, CFT_6_15, CFT_11_25, CFT_16_25, CFT_26_40, CFT_26_103,75_Galon
                                FROM dbo.Rate_Service_LCL_Min
                                Where IsNull(Update_Rate,'') = '' and PortD = " & PortD & " and PortT = " & PortT
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Select Case nCF
                Case 0, 1, 2, 3, 4, 5
                    Return ds.Tables(0).Rows(0).Item("CFT_0_5")
                Case 6 To 10 And ds.Tables(0).Rows(0).Item("CFT_6_10") > 0.00
                    Return ds.Tables(0).Rows(0).Item("CFT_6_10")
                Case 6 To 15 And ds.Tables(0).Rows(0).Item("CFT_6_15") > 0
                    Return ds.Tables(0).Rows(0).Item("CFT_6_15")
                Case 11 To 25 And ds.Tables(0).Rows(0).Item("CFT_11_25") > 0
                    Return ds.Tables(0).Rows(0).Item("CFT_11_25")
                Case 16 To 25 And ds.Tables(0).Rows(0).Item("CFT_16_25") > 0
                    Return ds.Tables(0).Rows(0).Item("CFT_16_25")
                Case 26 To 40 And ds.Tables(0).Rows(0).Item("CFT_26_40") > 0
                    Return ds.Tables(0).Rows(0).Item("CFT_26_40")
                Case 26 To 103 And ds.Tables(0).Rows(0).Item("CFT_26_103") > 0
                    Return ds.Tables(0).Rows(0).Item("CFT_26_103")
                Case Else
                    Return Rate
            End Select
        End If
        ds = Nothing
        strSQL = Nothing
        Return nMin
    End Function

    Public Function Rate_To_Get_Query(ByVal Contract As String, ByVal PortO As Integer, ByVal PortL As Integer, ByVal PortD As Integer, ByVal PortT As Integer, ByVal Comdty As Integer, ByVal Eq As String)
        Dim ds As New DataSet
        strSQL = "Select Top 1 1 as Query_Type From Rate_Services Where Contract_Number = '" & Trim(Contract) & "' and PortO_Number = " & PortO & " and PORTL_NUMBER = " & PortL & " and PORTD_NUMBER = " & PortD & " and PORTT_NUMBER = " & PortT & " and COMDTY_ID = " & Comdty & " and Equipment_Type = '" & Trim(Eq) & "'"
        ds = ws.GetDataset(strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return 1
        Else
            strSQL = "Select Top 1 2 as Query_Type From Rate_Services Where Contract_Number = '" & Trim(Contract) & "' and PortO_Number = 1 and PORTL_NUMBER = " & PortL & " and PORTD_NUMBER = " & PortD & " and PORTT_NUMBER = " & PortT & " and COMDTY_ID = " & Comdty & " and Equipment_Type = '" & Trim(Eq) & "'"
            ds.Clear()
            ds = ws.GetDataset(strConnect, strSQL, 1)
            If ds.Tables(0).Rows.Count > 0 Then
                Return 2
            Else
                strSQL = "Select Top 1 3 as Query_Type From Rate_Services Where Contract_Number = '" & Trim(Contract) & "' and PortO_Number = " & PortO & " and PORTL_NUMBER = " & PortL & " and PORTD_NUMBER = " & PortD & " and PORTT_NUMBER = 1 and COMDTY_ID = " & Comdty & " and Equipment_Type = '" & Trim(Eq) & "'"
                ds.Clear()
                ds = ws.GetDataset(strConnect, strSQL, 1)
                If ds.Tables(0).Rows.Count > 0 Then
                    Return 3
                Else
                    strSQL = "Select Top 1 4 as Query_Type From Rate_Services Where Contract_Number = '" & Trim(Contract) & "' and PortO_Number = 1 and PORTL_NUMBER = " & PortL & " and PORTD_NUMBER = " & PortD & " and PORTT_NUMBER = 1 and COMDTY_ID = " & Comdty & " and Equipment_Type = '" & Trim(Eq) & "'"
                    ds.Clear()
                    ds = ws.GetDataset(strConnect, strSQL, 1)
                    If ds.Tables(0).Rows.Count > 0 Then
                        Return 4
                    Else
                        ' ------- With Contract => Rate Default
                        If Len(Trim(Contract)) > 0 Then
                            strSQL = "Select Top 1 5 as Query_Type From Rate_Services Where PortO_Number = 1 and PORTL_NUMBER = " & PortL & " and PORTD_NUMBER = " & PortD & " and PORTT_NUMBER = " & PortT & " and COMDTY_ID = " & Comdty & " and Equipment_Type = '" & Trim(Eq) & "'"
                            ds.Clear()
                            ds = ws.GetDataset(strConnect, strSQL, 1)
                            If ds.Tables(0).Rows.Count > 0 Then
                                Return 5
                            Else
                                Return 0
                            End If
                        End If
                    End If

                End If

            End If
        End If
    End Function

#Region "Stuffing"
    Public Function Stufing_Posted(ByVal Stuffing_ID As Integer) As Boolean
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, "Select isnull(Posted,'') as Posted From STFHDR Where uid = " & Stuffing_ID, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            If Trim(ds.Tables(0).Rows(0).Item("Posted")) = "P" Then
                ds = Nothing
                Return True
            Else
                ds = Nothing
                Return False
            End If
        Else
            ds = Nothing
            Return False
        End If
    End Function

    Public Function Get_Stuffing_BK_Seq(ByVal BK As Integer) As Integer
        Dim nSeq As Integer = 1
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, "SELECT Top 1 BK_Lin FROM STFHdr where Booking_Number = " & BK & " Order By BK_Lin Desc", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            nSeq = ds.Tables(0).Rows(0).Item("BK_Lin") + 1
        End If
        Return nSeq
    End Function
#End Region

    Public Function Transshipment(ByVal PortL As Integer, ByVal PortD As Integer, ByVal dFrom As Date, ByVal dTo As Date) As DataSet
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Dim ds As New DataSet
        strSQL = "Select leg, route_id, PORT_NUMBER, Port_Name, PortT, Port_Transshipment, Route_ID_A, Route_ID_B, Vessel_Name, Est_Departure
                     From
                    (Select leg, route_id, t2.PortD_number as PORT_NUMBER, t2.PortD_Name as Port_Name, PortT, pt.Port_name as Port_Transshipment, Route_ID_A, Route_ID_B,
                        (Select Top 1 Vessel_Name From Sailing_Master Where (Sailing_Master.Route_ID = T2.Route_ID and Sailing_Master.Port = T2.PortD_Number) and (Sailing_Master.Est_Departure between '" & Format(dFrom, "'yyyy-MM-dd") & "' and '" & Format(dTo, "'yyyy-MM-dd") & "')  Order By Format(Est_Departure,'yyyy-MM-dd')) as Vessel_Name,
                        (Select Top 1 Est_Departure From Sailing_Master Where (Sailing_Master.Route_ID = T2.Route_ID and Sailing_Master.Port = T2.PortT) and (Sailing_Master.Est_Departure between '" & Format(dFrom, "yyyy-MM-dd") & "' and '" & Format(dTo, "'yyyy-MM-dd") & "')  Order by Format(Est_Departure,'yyyy-MM-dd')) as Est_Departure
                     From
                    (
                    SELECT 2 as Leg, rl.Route_ID, rl.PortD_number, rl.PortD_Name, t.Port_Transshipment as PortT, Route_ID_A, Route_ID_B
                      FROM  dbo.Route_DTL AS rl INNER JOIN
                          dbo.Transshipments AS t ON (t.Route_ID_A = rl.Route_ID or t.Route_ID_B = rl.Route_ID)
                    WHERE (rl.PortD_number = " & PortD & ")) as T2 Inner Join 
                      Ports as pt on pt.PORT_NUMBER = t2.PortT

                    union

                      Select 1 as leg,route_id, t1.PortL_number as PORT_NUMBER, t1.PortL_Name as Port_Name, PortT, pt.Port_name as Port_Transshipment, Route_ID_A, Route_ID_B,
                       (Select Top 1 Vessel_Name From Sailing_Master Where (Sailing_Master.Route_ID = T1.Route_ID and Sailing_Master.Port = T1.PortL_Number) and (Sailing_Master.Est_Departure between '" & Format(dFrom, "'yyyy-MM-dd") & "' and '" & Format(dTo, "'yyyy-MM-dd") & "')  Order By Format(Est_Departure,'yyyy-MM-dd')) as Vessel_Name,
                       (Select Top 1 Est_Departure From Sailing_Master Where (Sailing_Master.Route_ID = T1.Route_ID and Sailing_Master.Port = T1.PortT) and (Sailing_Master.Est_Departure between '" & Format(dFrom, "'yyyy-MM-dd") & "' and '" & Format(dTo, "'yyyy-MM-dd") & "')  Order by Format(Est_Departure,'yyyy-MM-dd')) as Est_Departure
                    From
                    (SELECT rl.Route_ID, rl.PortL_number, rl.PortL_Name, t.Port_Transshipment as PortT, t.Route_ID_A, t.Route_ID_B
                    FROM  dbo.Route_DTL AS rl INNER JOIN
                          dbo.Transshipments AS t ON (t.Route_ID_A = rl.Route_ID or t.Route_ID_B = rl.Route_ID)
                    WHERE (rl.PortL_number = " & PortL & ")
                    ) as T1 Inner Join 
                      Ports as pt on pt.PORT_NUMBER = t1.PortT) as T3
                      Order by Leg"
        ds = ws.GetDataset(md.strConnect, strSQL, 1)
        Return ds
        ds = Nothing
        Cursor.Current = System.Windows.Forms.Cursors.Default
    End Function

    ' ------- Change done on 09/15/2022
    Public Function GL_Account_Desc(ByVal Acc As String) As String
        strSQL = "SELECT DISTINCT DESC_ENG FROM dbo.GLACCT WHERE ACCOUNT = '" & Trim(Acc) & "'"
        Dim ds As New DataSet
        ds = ws.GetDataset(strConnect, strSQL, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return Trim(ds.Tables(0).Rows(0).Item("DESC_ENG"))
        Else
            Return ""
        End If
        ds = Nothing
    End Function

    ' ------- Change done on 09/27/2022, GL get Dpto Code From Dpto Name
    Public Function GL_Get_Dpto_Number_x_Name(ByVal name As String) As String
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT Dpto_Number FROM dbo.GL_Dpto where Dpto_Name = '" & Trim(Replace(name, "'", "''")) & "'", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Dpto_Number")
        Else
            Return ""
        End If
    End Function

    ' ------- Change done on 10/04/2022, GL get Dpto Name From Dpto Number
    Public Function GL_Get_Dpto_Name_from_Code(ByVal code As String) As String
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT Dpto_Name FROM dbo.GL_Dpto where Dpto_Number = '" & Trim(code) & "'", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Dpto_Name")
        Else
            Return ""
        End If
    End Function

    Public Function Month_Int(ByVal strMonth As String) As Integer
        Select Case Trim(strMonth)
            Case "January"
                Return 1
            Case "February"
                Return 2
            Case "March"
                Return 3
            Case "April"
                Return 4
            Case "May"
                Return 5
            Case "June"
                Return 6
            Case "July"
                Return 7
            Case "August"
                Return 8
            Case "September"
                Return 9
            Case "October"
                Return 10
            Case "November"
                Return 11
            Case "December"
                Return 12
        End Select
    End Function


    Private Sub Populate_TreeView_From_dataset()
        Try
            Dim TreeView1 As New TreeView
            Dim DSNWind As DataSet
            Dim CNnwind As New SqlClient.SqlConnection("Server=.\SQLExpress;AttachDbFilename=C:\SQL Server 2000 Sample Databases\NORTHWND.mdf;Database=dbname;Trusted_Connection=Yes;INITIAL CATALOG=northwind;") '<==== CHANGE HERE 
            Dim DACustomers As New SqlClient.SqlDataAdapter("SELECT CustomerID, CompanyName, ContactName, Country FROM customers WHERE country = 'Germany'", CNnwind)
            Dim DAOrders As New SqlClient.SqlDataAdapter("SELECT CustomerID, OrderID, OrderDate, ShippedDate, ShipVia, Freight FROM orders where customerid in (select customerid from customers where country = 'Germany')", CNnwind)
            Dim DAOrderDetails As New SqlClient.SqlDataAdapter("Select * from [Order Details] where OrderID in (SELECT OrderID FROM orders where customerid in (select customerid from customers where country = 'Germany'))", CNnwind)

            DSNWind = New DataSet()
            CNnwind.Open()
            DACustomers.Fill(DSNWind, "dtCustomers")
            DAOrders.Fill(DSNWind, "dtOrders")
            DAOrderDetails.Fill(DSNWind, "dtOrderDetails")
            'Close the connection to the data store; free up the resources
            CNnwind.Close()

            'Create a data relation object to facilitate the relationship between the Customers and Orders data tables.
            DSNWind.Relations.Add("CustToOrd", DSNWind.Tables("dtCustomers").Columns("CustomerID"), DSNWind.Tables("dtOrders").Columns("CustomerID"))
            DSNWind.Relations.Add("OrdToDet", DSNWind.Tables("dtOrders").Columns("OrderID"), DSNWind.Tables("dtOrderdetails").Columns("OrderID"))
            '''''''''''''''''''''''
            TreeView1.Nodes.Clear()
            'Dim i, n As Integer - These variables are unused so commented out.
            Dim parentrow As DataRow
            Dim ParentTable As DataTable
            ParentTable = DSNWind.Tables("dtCustomers")

            For Each parentrow In ParentTable.Rows
                Dim parentnode As TreeNode
                parentnode = New TreeNode(CStr(parentrow.Item(0)))
                TreeView1.Nodes.Add(parentnode)
                ''''populate child'''''
                '''''''''''''''''''''''
                Dim childrow As DataRow
                Dim childnode As TreeNode
                childnode = New TreeNode()
                For Each childrow In parentrow.GetChildRows("CustToOrd")
                    childnode = parentnode.Nodes.Add(childrow(0).ToString & " " & childrow(1).ToString & " " & childrow(2).ToString)
                    childnode.Tag = childrow("OrderID")
                    ''''populate child2''''
                    ''''''''''''''''''''''''''
                    Dim childrow2 As DataRow
                    Dim childnode2 As TreeNode
                    childnode2 = New TreeNode()
                    For Each childrow2 In childrow.GetChildRows("OrdToDet")
                        childnode2 = childnode.Nodes.Add(childrow2(0).ToString)

                    Next childrow2
                    ''''''''''''''''''''''''

                Next childrow
                '''''''''''''''
            Next parentrow
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try

    End Sub

    Public Function Printer_Name_From_Machine(ByVal ComputerName As String) As String
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT isnull(Printer_Name,'') as Printer_name FROM dbo.Version_Machine WHERE (isnull(MachineName,'') = '" & Trim(ComputerName) & "')", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return ds.Tables(0).Rows(0).Item("Printer_name")
        Else
            Return ""
        End If
    End Function


    Public Function Loaders(ByVal uid As Integer) As String
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT uid, Loader FROM dbo.STF_Loaders where uid = " & uid, 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return Trim(ds.Tables(0).Rows(0).Item("Loader"))
        Else
            Return ""
        End If
    End Function
    Public Function Loader_ID(ByVal Loader As String) As Integer
        Dim ds As New DataSet
        ds = ws.GetDataset(md.strConnect, "SELECT uid, Loader FROM dbo.STF_Loaders where loader = '" & Trim(Loader) & "'", 1)
        If ds.Tables(0).Rows.Count > 0 Then
            Return Trim(ds.Tables(0).Rows(0).Item("uid"))
        Else
            Return 0
        End If
    End Function

End Module

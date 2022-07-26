Public Class SampleLog
    Public Function GetSampleLogging(Byval Reassign As Boolean , ByVal searchfilter As String, ByVal userid As String, ByVal category As String, ByVal labvalue As String) As String
        Dim strCondition As String = ""
        Dim strSQL As String = ""
        If searchfilter <> "" Then 'run when the user input a search key
            'run when search on lab or all lab
            strSQL = " SELECT ST_SAMPLE_ID,ST_CLIENT_ID,ST_SAMPLE_NO,ST_LAB_ID,ST_BATCH_NO,ST_SAMPLE_DESC,ST_STATUS,ST_SAMPLE_REQUIRED,ST_DATAGROUP_NAME,ST_DUE_DT,ST_FLOW_IND,ST_RCVDAT,C.CSM_CODE_NAME,ST_CN_NO,ST_YEAR FROM SAMPLE_TXN S,CODEs_MSTR C" &
                     " WHERE S.ST_FLOW_IND =C.CSM_CODE_ID AND C.CSM_CATEGORY ='SFlow' " &
                     " AND (ST_DATAGROUP_NAME IN" & category &
                     " OR ST_SAMPLE_NO IN " &
                     " (SELECT DISTINCT(CT_SAMPLE_NO) FROM COMPONENT_TXN WHERE CT_CHEMIST ='" & userid & "'))" &
                     " AND ST_DATAGROUP_NAME IN" & category & " AND" &
                     " ST_STATUS  IN ('DRAFT')  "
            If labvalue <> "All Lab" Then
                strSQL = strSQL & " AND ST_LAB_ID" &
                         "='" & labvalue & "'"
            End If
            If Reassign = True Then
                strSQL = strSQL & " AND ST_FLOW_IND =2"
            End If
            strSQL = strSQL & " ORDER BY ST_SAMPLE_ID ASC "

        End If
        GetSampleLogging = strSQL
    End Function


End Class

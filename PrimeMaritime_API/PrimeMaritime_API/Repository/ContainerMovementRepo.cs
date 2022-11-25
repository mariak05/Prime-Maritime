﻿using PrimeMaritime_API.Helpers;
using PrimeMaritime_API.Models;
using PrimeMaritime_API.Translators;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeMaritime_API.Repository
{
    public class ContainerMovementRepo
    {
        public void InsertContainerMovement(string connstring, CONTAINER_MOVEMENT request,bool fromXL)
        {
            

            if (request.CONTAINER_NO != "")
            {
                
                SqlParameter[] parameters =
                {
                  new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "GET_CONTAINER_MOVEMENT" },
                  new SqlParameter("@CONTAINER_NO", SqlDbType.VarChar, 100) { Value = request.CONTAINER_NO }
                };

                var cmID= SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_CONTAINER_MOVEMENT", parameters);
                if (cmID != "")
                {
                    DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "SP_CRUD_CONTAINER_MOVEMENT", parameters);
                    List<CM> containerMovementList = SqlHelper.CreateListFromTable<CM>(dataTable);

                    foreach (var i in containerMovementList)
                    {
                        i.BOOKING_NO = request.BOOKING_NO;
                        i.CRO_NO = request.CRO_NO;
                        i.CONTAINER_NO = request.CONTAINER_NO;
                        i.ACTIVITY = request.ACTIVITY;
                        i.PREV_ACTIVITY = i.ACTIVITY;
                        i.ACTIVITY_DATE = request.ACTIVITY_DATE;
                        i.LOCATION = request.LOCATION;
                        i.STATUS = request.STATUS;
                        i.AGENT_CODE = request.AGENT_CODE;
                        i.DEPO_CODE = request.DEPO_CODE;
                        i.CREATED_BY = request.CREATED_BY;
                    }

                    string[] columns = new string[11];
                    columns[0] = "BOOKING_NO";
                    columns[1] = "CRO_NO";
                    columns[2] = "CONTAINER_NO";
                    columns[3] = "ACTIVITY";
                    columns[4] = "PREV_ACTIVITY";
                    columns[5] = "ACTIVITY_DATE";
                    columns[6] = "LOCATION";
                    columns[7] = "STATUS";
                    columns[8] = "AGENT_CODE";
                    columns[9] = "DEPO_CODE";
                    columns[10] = "CREATED_BY";

                    SqlHelper.UpdateCMData<CM>(containerMovementList, "TB_CONTAINER_MOVEMENT", connstring, columns);
                }
                else
                {
                    SqlParameter[] paramS =
                    {
                      new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "CREATE_CONTAINER_MOVEMENT" },
                      new SqlParameter("@BOOKING_NO", SqlDbType.VarChar, 100) { Value = request.BOOKING_NO },
                      new SqlParameter("@CRO_NO", SqlDbType.VarChar, 100) { Value = request.CRO_NO },
                      new SqlParameter("@CONTAINER_NO", SqlDbType.VarChar, 100) { Value = request.CONTAINER_NO },
                      new SqlParameter("@ACTIVITY", SqlDbType.VarChar, 50) { Value = request.ACTIVITY },
                      new SqlParameter("@PREV_ACTIVITY", SqlDbType.VarChar, 50) { Value = request.PREV_ACTIVITY },
                      new SqlParameter("@ACTIVITY_DATE", SqlDbType.DateTime) { Value = request.ACTIVITY_DATE },
                      new SqlParameter("@LOCATION", SqlDbType.VarChar, 100) { Value = request.LOCATION },
                      new SqlParameter("@STATUS", SqlDbType.VarChar,50) { Value = request.STATUS },
                      new SqlParameter("@AGENT_CODE", SqlDbType.VarChar, 20) { Value = request.AGENT_CODE },
                      new SqlParameter("@DEPO_CODE", SqlDbType.VarChar, 20) { Value = request.DEPO_CODE },
                      new SqlParameter("@CREATED_BY", SqlDbType.VarChar,255) { Value = request.CREATED_BY }
                    };

                    var newCmID = SqlHelper.ExecuteProcedureReturnString(connstring, "SP_CRUD_CONTAINER_MOVEMENT", paramS);

                }
                
            }
            else
            {
                if (fromXL == true)
                {
                    DataTable tbl = new DataTable();
                    tbl.Columns.Add(new DataColumn("BOOKING_NO", typeof(string)));
                    tbl.Columns.Add(new DataColumn("CRO_NO", typeof(string)));
                    tbl.Columns.Add(new DataColumn("CONTAINER_NO", typeof(string)));
                    tbl.Columns.Add(new DataColumn("ACTIVITY", typeof(string)));
                    tbl.Columns.Add(new DataColumn("PREV_ACTIVITY", typeof(string)));
                    tbl.Columns.Add(new DataColumn("ACTIVITY_DATE", typeof(DateTime)));
                    tbl.Columns.Add(new DataColumn("LOCATION", typeof(string)));
                    tbl.Columns.Add(new DataColumn("STATUS", typeof(string)));
                    tbl.Columns.Add(new DataColumn("AGENT_CODE", typeof(string)));
                    tbl.Columns.Add(new DataColumn("DEPO_CODE", typeof(string)));
                    tbl.Columns.Add(new DataColumn("CREATED_BY", typeof(string)));

                    foreach (var i in request.CONTAINER_MOVEMENT_LIST)
                    {
                        DataRow dr = tbl.NewRow();

                        dr["BOOKING_NO"] = i.BOOKING_NO;
                        dr["CRO_NO"] = i.CRO_NO;
                        dr["CONTAINER_NO"] = i.CONTAINER_NO;
                        dr["ACTIVITY"] = i.ACTIVITY;
                        dr["PREV_ACTIVITY"] = i.PREV_ACTIVITY;
                        dr["ACTIVITY_DATE"] = i.ACTIVITY_DATE;
                        dr["LOCATION"] = i.LOCATION;
                        dr["STATUS"] = i.STATUS;
                        dr["AGENT_CODE"] = i.AGENT_CODE;
                        dr["DEPO_CODE"] = i.DEPO_CODE;
                        dr["CREATED_BY"] = i.CREATED_BY;

                        tbl.Rows.Add(dr);
                    }

                    string[] columns = new string[11];
                    columns[0] = "BOOKING_NO";
                    columns[1] = "CRO_NO";
                    columns[2] = "CONTAINER_NO";
                    columns[3] = "ACTIVITY";
                    columns[4] = "PREV_ACTIVITY";
                    columns[5] = "ACTIVITY_DATE";
                    columns[6] = "LOCATION";
                    columns[7] = "STATUS";
                    columns[8] = "AGENT_CODE";
                    columns[9] = "DEPO_CODE";
                    columns[10] = "CREATED_BY";

                    SqlHelper.ExecuteProcedureBulkInsert(connstring, tbl, "TB_CONTAINER_MOVEMENT", columns);

                }
                else
                {
                    //No need to bind other list properties at backend
                    foreach (var i in request.CONTAINER_MOVEMENT_LIST)
                    {
                        i.BOOKING_NO = request.BOOKING_NO;
                        i.CRO_NO = request.CRO_NO;
                        i.ACTIVITY_DATE = request.ACTIVITY_DATE;
                        i.LOCATION = request.LOCATION;
                        i.AGENT_CODE = request.AGENT_CODE;
                        i.DEPO_CODE = request.DEPO_CODE;
                        i.CREATED_BY = request.CREATED_BY;
                    }

                    string[] columns = new string[11];
                    columns[0] = "BOOKING_NO";
                    columns[1] = "CRO_NO";
                    columns[2] = "CONTAINER_NO";
                    columns[3] = "ACTIVITY";
                    columns[4] = "PREV_ACTIVITY";
                    columns[5] = "ACTIVITY_DATE";
                    columns[6] = "LOCATION";
                    columns[7] = "STATUS";
                    columns[8] = "AGENT_CODE";
                    columns[9] = "DEPO_CODE";
                    columns[10] = "CREATED_BY";

                    SqlHelper.UpdateCMData<CM>(request.CONTAINER_MOVEMENT_LIST, "TB_CONTAINER_MOVEMENT", connstring, columns);
                }
                

            }
        }
        public List<CONTAINER_MOVEMENT> GetContainerMovementList(string connstring, string AGENT_CODE, string DEPO_CODE,string BOOKING_NO, string CRO_NO, string CONTAINER_NO)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@OPERATION", SqlDbType.VarChar,50) { Value = "GET_CONTAINER_MOVEMENT" },
                new SqlParameter("@AGENT_CODE", SqlDbType.VarChar,20) { Value = AGENT_CODE },
                new SqlParameter("@DEPO_CODE", SqlDbType.VarChar,20) { Value = DEPO_CODE },
                new SqlParameter("@BOOKING_NO", SqlDbType.VarChar,100) { Value = BOOKING_NO },
                new SqlParameter("@CRO_NO", SqlDbType.VarChar,100) { Value = CRO_NO },
                new SqlParameter("@CONTAINER_NO", SqlDbType.VarChar,100) { Value = CONTAINER_NO }
             };
            DataTable dataTable = SqlHelper.ExtecuteProcedureReturnDataTable(connstring, "SP_CRUD_CONTAINER_MOVEMENT", parameters);
            List<CONTAINER_MOVEMENT> containerList = SqlHelper.CreateListFromTable<CONTAINER_MOVEMENT>(dataTable);
            return containerList;
        }

        public CM GetSingleContainerMovement(string connstring, string CONTAINER_NO)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@OPERATION", SqlDbType.VarChar, 50) { Value = "GET_SINGLE_CONTAINER_MOVEMENT" },
                    new SqlParameter("@CONTAINER_NO", SqlDbType.VarChar, 100) { Value = CONTAINER_NO }
                };

                return SqlHelper.ExtecuteProcedureReturnData<CM>(connstring, "SP_CRUD_CONTAINER_MOVEMENT", r => r.TranslateCM(), parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
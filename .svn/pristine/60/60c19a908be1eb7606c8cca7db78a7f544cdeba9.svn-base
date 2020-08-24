using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Runtime.InteropServices;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using JEWELRY_NUBES.Util;

namespace JEWELRY_NUBES.Util
{
    class ExcelDownloadUtil
    {
        #region DOWNLOAD EXCEL LOG BOOK
        public int DownloadLogBookByExcel(JArray param)
        {
            int nRet = 0;
            try
            {
                string toDate = DateTime.Now.ToString("yyyyMMddHHmmss");
                SaveFileDialog saveFileDlg = new SaveFileDialog();
                saveFileDlg.FileName = "Logbook." + toDate + ".xls";
                saveFileDlg.Filter = "Excel Files|*.xls";

                if (saveFileDlg.ShowDialog() == DialogResult.OK)
                {
                    using (FileStream f = new FileStream(saveFileDlg.FileName, FileMode.Create, FileAccess.ReadWrite))
                    {
                        HSSFWorkbook wb = GenerateExcelToStream(param);
                        wb.Write(f);
                    }
                }
                else
                {
                    nRet = 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                nRet = -1;
            }

            return nRet;
        }

        public static void ReleaseExcelObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null;
                throw ex;
            }
            finally
            {
                GC.Collect();
            }
        }

        private int columnCnt = 13;

        private string[] logBookHeaderTexts = new string[]
        {
            "Date of Invoice ",
            "Invoice Number ",
            "GST Charged ",
            "Document ID ",
            "Date of Issuing eTRS Ticket ",
            "Time of Issuing eTRS ticket ",
            "Passport number ",
            "Name of Staff ",
            "Signature of Staff ",
            "Void/Issued ",
            "Total value of the sales made to the tourist under TRS for the preceding three (3) months at the point of sales is $7,000 or more [Yes/No] ",
            "Are goods packed in sealed bag? [Yes/No] ",
            "Serial number of the sealed bag (if applicable) "
        };

        // 7000 = 너비 26.71
        private int[] logBookHeaderSizes = new int[] {
            3500,
            7500,
            3500,
            6500,
            3500,
            3500,
            3500,
            6000,
            7000,
            3000,
            7000,
            4500,
            6500
        };

        public HSSFWorkbook GenerateExcelToStream(JArray param)
        {
            //Excel
            HSSFWorkbook wb1 = new HSSFWorkbook();

            HSSFSheet sheet1 = (HSSFSheet)wb1.CreateSheet("Sheet1");

            HSSFRow row;

            int dataStrIdx = 3;

            try
            {
                IFont fTitle = wb1.CreateFont();
                fTitle.Boldweight = (short)FontBoldWeight.Bold;
                fTitle.FontHeightInPoints = 16;

                IFont fHeader = wb1.CreateFont();
                fHeader.FontHeightInPoints = 10;

                ICellStyle sTitle = wb1.CreateCellStyle();
                sTitle.SetFont(fTitle);
                sTitle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;
                sTitle.VerticalAlignment = VerticalAlignment.Center;

                ICellStyle sHeader = wb1.CreateCellStyle();
                sHeader.SetFont(fHeader);
                sHeader.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;
                sHeader.VerticalAlignment = VerticalAlignment.Bottom;
                sHeader.WrapText = true;
                sHeader.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                sHeader.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                sHeader.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                sHeader.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                
                ICellStyle sDL = wb1.CreateCellStyle();
                sDL.SetFont(fHeader);
                sDL.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;
                sDL.VerticalAlignment = VerticalAlignment.Center;
                sDL.WrapText = true;
                sDL.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                sDL.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                sDL.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                sDL.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;

                ICellStyle sDC = wb1.CreateCellStyle();
                sDC.SetFont(fHeader);
                sDC.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
                sDC.VerticalAlignment = VerticalAlignment.Center;
                sDC.WrapText = true;
                sDC.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                sDC.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                sDC.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                sDC.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;

                ICellStyle sDR = wb1.CreateCellStyle();
                sDR.SetFont(fHeader);
                sDR.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Right;
                sDR.VerticalAlignment = VerticalAlignment.Center;
                sDR.WrapText = true;
                sDR.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
                sDR.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                sDR.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                sDR.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;

                for (int i = 0; i < columnCnt; i++)
                {
                    sheet1.SetColumnWidth(i, logBookHeaderSizes[i]);
                }

                /*
                IDataFormat dataFormatCustom = workbook.CreateDataFormat();
                cell.CellStyle = styles["cell"];
                cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("yyyyMMdd HH:mm:ss");
                */

                row = (HSSFRow)sheet1.CreateRow(0);
                row.Height = 800;
                row.CreateCell(0);
                row.Cells[0].CellStyle = sTitle;
                row.Cells[0].SetCellValue("Transaction LogBook");
                
                row = null;
                row = (HSSFRow)sheet1.CreateRow(1);
                row.Height = 300;

                row = null;
                row = (HSSFRow)sheet1.CreateRow(2);
                row.Height = 1400;

                for (int c = 0; c < columnCnt; c++)
                {
                    ICell nCell = row.CreateCell(c);
                    nCell.SetCellValue(logBookHeaderTexts[c]);
                    nCell.CellStyle = sHeader;
                }
                
                for (int r = 0; r < param.Count; r++)
                {
                    JObject json = (JObject)param[r];

                    row = null;
                    row = (HSSFRow)sheet1.CreateRow(dataStrIdx + r);

                    for (int c = 0; c < columnCnt; c++)
                    {
                        row.CreateCell(c);
                        switch (c)
                        {
                            case 0:
                                row.Cells[c].CellStyle = sDC;
                                row.Cells[c].SetCellValue(Util.Utils.FormatDate(json["INVOICE_DATE"].ToString(), 2));
                                break;
                            case 1:
                                row.Cells[c].CellStyle = sDL;
                                row.Cells[c].SetCellValue(json["RCT_NO"].ToString());
                                break;
                            case 2:
                                row.Cells[c].CellStyle = sDR;
                                row.Cells[c].SetCellValue(double.Parse(json["GST_AMT"].ToString()));
                                break;
                            case 3:
                                row.Cells[c].CellStyle = sDL;
                                row.Cells[c].SetCellValue(Util.Utils.FormatDocId(json["DOC_ID"].ToString()));
                                break;
                            case 4:
                                row.Cells[c].CellStyle = sDC;
                                row.Cells[c].SetCellValue(Util.Utils.FormatDate(json["ISSUE_DATE"].ToString(), 2));
                                break;
                            case 5:
                                row.Cells[c].CellStyle = sDC;
                                row.Cells[c].SetCellValue(Util.Utils.FormatTime(json["ISSUE_TIME"].ToString(), "12"));
                                break;
                            case 6:
                                row.Cells[c].CellStyle = sDC;
                                row.Cells[c].SetCellValue(json["PASSPORT_NO"].ToString());
                                break;
                            case 7:
                                row.Cells[c].CellStyle = sDL;
                                row.Cells[c].SetCellValue(json["STAFF_NAME"].ToString());
                                break;
                            case 8:
                                row.Cells[c].CellStyle = sDL;
                                break;
                            case 9:
                                row.Cells[c].CellStyle = sDC;
                                string status = json["STATUS_DESC"].ToString();
                                if (status.Equals("Voided"))
                                {
                                    row.Cells[c].SetCellValue(status + " (" + Util.Utils.FormatDate(json["VOID_DATE"].ToString(), 2) + ")");
                                }
                                else
                                {
                                    row.Cells[c].SetCellValue(status);
                                }
                                break;
                            case 10:
                                row.Cells[c].CellStyle = sDC;
                                row.Cells[c].SetCellValue((json["SEAL_BAG_YN"].ToString() == "Y" ? "Yes" : "No"));
                                break;
                            case 11:
                                row.Cells[c].CellStyle = sDC;
                                row.Cells[c].SetCellValue((json["PACKED_YN"].ToString() == "Y" ? "Yes" : "No"));
                                break;
                            case 12:
                                row.Cells[c].CellStyle = sDL;
                                row.Cells[c].SetCellValue(json["SEAL_BAG_NO"].ToString());
                                break;
                        }
                    }
                }
                return wb1;
            }
            catch (Exception ex)
            {
                Constants.LOGGER_MAIN.Error(ex.Message);
                throw ex;
            }
        }
        #endregion
    }
}

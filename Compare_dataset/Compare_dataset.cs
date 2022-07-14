using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Compare_dataset
{
    public class Compare_dataset
    {
        List<CAN_Signals.Signal> Signals;
        CAN_Signals.MCRRS_SIGNALS Signal_List = new CAN_Signals.MCRRS_SIGNALS();
        List<ushort> MessageIDs;
        DataBinary.RawFile raw;
        DataBinary.RawFile msgoneHundred;
        DataBinary.RawFile msgtwoHundred;
        DataBinary.RawFile msgfiveHundred;
        DataBinary.RawFile msgtwoHundredandSix;
        DataBinary.RawFile msgttwuHundredandSeven;
        DataBinary.RawFile msgtwoHundredandtwo;
        DataBinary.RawFile msgtwoHundredandThree;
        DataBinary.RawFile msgtwoHundredandEight;
        DataBinary.RawFile msgtwoHundredandOne;
        DataBinary.RawFile msgtwoHundredandFive;
        DataBinary.RawFile msgtwoHundredFour;
        string sPath;
        int[] DataSetIndices;
        string[] ValueNames;

        string[] m_outputString;
        long outputstringCount;

        int bAllValid;

        /// <summary>
        /// Holds the first date that occurs in this file
        /// </summary>
        public string fileStartDate { get; set; }

        /// <summary>
        /// Holds the last date in the file
        /// </summary>
        public string fileEnddate { get; set; }

        public string[] getResult()
        {
            return m_outputString;
        }
        public string[] getValrName()
        {
            return ValueNames;
        }

        public long count { get; set; }
        public List<CAN_Signals.Signal> getIDs()
        {
            List<CAN_Signals.Signal> output = Signal_List.getList();

            return output;
        }
        public Compare_dataset(string path, int[] ids)
        {
            Signals = Signal_List.getList();
            MessageIDs = Signal_List.mcrrs_ids();
            bAllValid = 1;
            if (File.Exists(path))
            {
                sPath = path;
            }
            else
            {
                bAllValid = 0;
            }

            if (ids.Length >= 1)
            {
                DataSetIndices = ids;
            }
            else
            {
                bAllValid = 0;
            }

            ValueNames = new string[DataSetIndices.Length];


            raw = new DataBinary.RawFile(sPath);



            msgoneHundred = raw.getMsgSet(100, true);
            msgtwoHundred = raw.getMsgSet(200, true);
            msgfiveHundred = raw.getMsgSet(500, true);
            msgtwoHundredandSix = raw.getMsgSet(206, true);
            msgttwuHundredandSeven = raw.getMsgSet(207, true);
            msgtwoHundredandtwo = raw.getMsgSet(202, true);
            msgtwoHundredandThree = raw.getMsgSet(203, true);
            msgtwoHundredandEight = raw.getMsgSet(208, true);
            msgtwoHundredandOne = raw.getMsgSet(201, true);
            msgtwoHundredandFive = raw.getMsgSet(205, true);
            msgtwoHundredFour = raw.getMsgSet(204, true);

            this.fileStartDate = raw.FileStartDate;
            this.fileEnddate = raw.FileEnddate;
            this.outputstringCount = getRecordCount();

            m_outputString = new string[outputstringCount];


            if (raw.RecordCount < 1)
            {
                bAllValid = 0;
            }




            if (bAllValid == 1)
            {

                loadDateField();
                loadDatasets();

                string finished = "";

            }
        }



        private long getRecordCount()
        {
            long outputstringCount2 = msgoneHundred.RecordCount;

            if (outputstringCount2 > msgtwoHundred.RecordCount)
            {
                outputstringCount2 = msgtwoHundred.RecordCount;
            }

            if (outputstringCount2 > msgfiveHundred.RecordCount)
            {
                outputstringCount2 = msgfiveHundred.RecordCount;
            }

            if (outputstringCount2 > msgtwoHundredandSix.RecordCount)
            {
                outputstringCount2 = msgtwoHundredandSix.RecordCount;
            }

            if (outputstringCount2 > msgttwuHundredandSeven.RecordCount)
            {
                outputstringCount2 = msgttwuHundredandSeven.RecordCount;
            }

            if (outputstringCount2 > msgtwoHundredandtwo.RecordCount)
            {
                outputstringCount2 = msgtwoHundredandtwo.RecordCount;
            }

            if (outputstringCount2 > msgtwoHundredandThree.RecordCount)
            {
                outputstringCount2 = msgtwoHundredandThree.RecordCount;
            }

            if (outputstringCount2 > msgtwoHundredandEight.RecordCount)
            {
                outputstringCount2 = msgtwoHundredandEight.RecordCount;
            }
            if (outputstringCount2 > msgtwoHundredandOne.RecordCount)
            {
                outputstringCount2 = msgtwoHundredandOne.RecordCount;
            }

            if (outputstringCount2 > msgtwoHundredandFive.RecordCount)
            {
                outputstringCount2 = msgtwoHundredandFive.RecordCount;
            }

            if (outputstringCount2 > msgtwoHundredFour.RecordCount)
            {
                outputstringCount2 = msgtwoHundredFour.RecordCount;
            }

            this.count = outputstringCount2;
            return outputstringCount2;
        }

        private void loadDateField()
        {
            for (int i = 0; i < this.outputstringCount; i++)
            {
                m_outputString[i] = msgoneHundred.Msgtime[i].ToString() + '|';
            }
        }


        private void loadDatasets()
        {
            string Message_ID = "";
            int Can_Frame = 0;
            int Lo_Bit = 0;
            int Hi_Bit = 0;
            int iBit = 0;
            string sName = "";
            int iScale = 0;



            string str = "CAN_Signals.SixteenBitSignal";
            string str2 = "CAN_Signals.EightBitSignal";
            string str3 = "CAN_Signals.BoolSignal";

            for (int i = 0; i < this.DataSetIndices.Length; i++)
            {
                //string strtext = Signals[DataSetIndices[i]].GetType().ToString();
                if (Signals[DataSetIndices[i]].GetType().ToString() == str)
                {
                    CAN_Signals.SixteenBitSignal temp = (CAN_Signals.SixteenBitSignal)Signals[DataSetIndices[i]];


                    Message_ID = temp.msgID;
                    Can_Frame = 0;
                    Lo_Bit = Convert.ToInt32(temp.LSB.Substring(1, 1));
                    Hi_Bit = Convert.ToInt32(temp.MSB.Substring(1, 1));
                    iBit = 0;
                    iScale = temp.Scale;
                    sName = temp.Name;

                    ValueNames[i] = temp.Name;
                    for (int j = 0; j < this.outputstringCount; j++)
                    {
                        if (temp.msgID == "100")
                        {
                            byte[] tempdata = new byte[8];
                            tempdata[0] = msgoneHundred.F0[j];
                            tempdata[1] = msgoneHundred.F1[j];
                            tempdata[2] = msgoneHundred.F2[j];
                            tempdata[3] = msgoneHundred.F3[j];
                            tempdata[4] = msgoneHundred.F4[j];
                            tempdata[5] = msgoneHundred.F5[j];
                            tempdata[6] = msgoneHundred.F6[j];
                            tempdata[7] = msgoneHundred.F7[j];

                            // tempdata[Hi_Bit] << 8 | tempdata[Lo_Bit] & 0xFF
                            var temp2 = tempdata[Hi_Bit] << 8 | tempdata[Lo_Bit] & 0xFF;
                            this.m_outputString[j] = this.m_outputString[j] + (tempdata[Hi_Bit] << 8 | tempdata[Lo_Bit] & 0xFF).ToString() + '|';
                        }
                        else if (Message_ID == "200")
                        {
                            byte[] tempdata = new byte[8];
                            tempdata[0] = msgtwoHundred.F0[j];
                            tempdata[1] = msgtwoHundred.F1[j];
                            tempdata[2] = msgtwoHundred.F2[j];
                            tempdata[3] = msgtwoHundred.F3[j];
                            tempdata[4] = msgtwoHundred.F4[j];
                            tempdata[5] = msgtwoHundred.F5[j];
                            tempdata[6] = msgtwoHundred.F6[j];
                            tempdata[7] = msgtwoHundred.F7[j];


                            this.m_outputString[j] = this.m_outputString[j] + (tempdata[Hi_Bit] << 8 | tempdata[Lo_Bit] & 0xFF).ToString() + '|';
                        }
                        else if (Message_ID == "500")
                        {
                            byte[] tempdata = new byte[8];
                            tempdata[0] = msgfiveHundred.F0[j];
                            tempdata[1] = msgfiveHundred.F1[j];
                            tempdata[2] = msgfiveHundred.F2[j];
                            tempdata[3] = msgfiveHundred.F3[j];
                            tempdata[4] = msgfiveHundred.F4[j];
                            tempdata[5] = msgfiveHundred.F5[j];
                            tempdata[6] = msgfiveHundred.F6[j];
                            tempdata[7] = msgfiveHundred.F7[j];


                            this.m_outputString[j] = this.m_outputString[j] + (tempdata[Hi_Bit] << 8 | tempdata[Lo_Bit] & 0xFF).ToString() + '|';
                        }
                        else if (Message_ID == "206")
                        {
                            byte[] tempdata = new byte[8];
                            tempdata[0] = msgtwoHundredandSix.F0[j];
                            tempdata[1] = msgtwoHundredandSix.F1[j];
                            tempdata[2] = msgtwoHundredandSix.F2[j];
                            tempdata[3] = msgtwoHundredandSix.F3[j];
                            tempdata[4] = msgtwoHundredandSix.F4[j];
                            tempdata[5] = msgtwoHundredandSix.F5[j];
                            tempdata[6] = msgtwoHundredandSix.F6[j];
                            tempdata[7] = msgtwoHundredandSix.F7[j];


                            this.m_outputString[j] = this.m_outputString[j] + (tempdata[Hi_Bit] << 8 | tempdata[Lo_Bit] & 0xFF).ToString() + '|';
                        }
                        else if (Message_ID == "207")
                        {
                            byte[] tempdata = new byte[8];
                            tempdata[0] = msgttwuHundredandSeven.F0[j];
                            tempdata[1] = msgttwuHundredandSeven.F1[j];
                            tempdata[2] = msgttwuHundredandSeven.F2[j];
                            tempdata[3] = msgttwuHundredandSeven.F3[j];
                            tempdata[4] = msgttwuHundredandSeven.F4[j];
                            tempdata[5] = msgttwuHundredandSeven.F5[j];
                            tempdata[6] = msgttwuHundredandSeven.F6[j];
                            tempdata[7] = msgttwuHundredandSeven.F7[j];


                            this.m_outputString[j] = this.m_outputString[j] + (tempdata[Hi_Bit] << 8 | tempdata[Lo_Bit] & 0xFF).ToString() + '|';
                        }
                        else if (Message_ID == "202")
                        {
                            byte[] tempdata = new byte[8];
                            tempdata[0] = msgtwoHundredandtwo.F0[j];
                            tempdata[1] = msgtwoHundredandtwo.F1[j];
                            tempdata[2] = msgtwoHundredandtwo.F2[j];
                            tempdata[3] = msgtwoHundredandtwo.F3[j];
                            tempdata[4] = msgtwoHundredandtwo.F4[j];
                            tempdata[5] = msgtwoHundredandtwo.F5[j];
                            tempdata[6] = msgtwoHundredandtwo.F6[j];
                            tempdata[7] = msgtwoHundredandtwo.F7[j];


                            this.m_outputString[j] = this.m_outputString[j] + (tempdata[Hi_Bit] << 8 | tempdata[Lo_Bit] & 0xFF).ToString() + '|';
                        }
                        else if (Message_ID == "203")
                        {
                            byte[] tempdata = new byte[8];
                            tempdata[0] = msgtwoHundredandThree.F0[j];
                            tempdata[1] = msgtwoHundredandThree.F1[j];
                            tempdata[2] = msgtwoHundredandThree.F2[j];
                            tempdata[3] = msgtwoHundredandThree.F3[j];
                            tempdata[4] = msgtwoHundredandThree.F4[j];
                            tempdata[5] = msgtwoHundredandThree.F5[j];
                            tempdata[6] = msgtwoHundredandThree.F6[j];
                            tempdata[7] = msgtwoHundredandThree.F7[j];


                            this.m_outputString[j] = this.m_outputString[j] + (tempdata[Hi_Bit] << 8 | tempdata[Lo_Bit] & 0xFF).ToString() + '|';
                        }
                        else if (Message_ID == "208")
                        {
                            byte[] tempdata = new byte[8];
                            tempdata[0] = msgtwoHundredandEight.F0[j];
                            tempdata[1] = msgtwoHundredandEight.F1[j];
                            tempdata[2] = msgtwoHundredandEight.F2[j];
                            tempdata[3] = msgtwoHundredandEight.F3[j];
                            tempdata[4] = msgtwoHundredandEight.F4[j];
                            tempdata[5] = msgtwoHundredandEight.F5[j];
                            tempdata[6] = msgtwoHundredandEight.F6[j];
                            tempdata[7] = msgtwoHundredandEight.F7[j];


                            this.m_outputString[j] = this.m_outputString[j] + (tempdata[Hi_Bit] << 8 | tempdata[Lo_Bit] & 0xFF).ToString() + '|';
                        }
                        else if (Message_ID == "201")
                        {
                            byte[] tempdata = new byte[8];
                            tempdata[0] = msgtwoHundredandOne.F0[j];
                            tempdata[1] = msgtwoHundredandOne.F1[j];
                            tempdata[2] = msgtwoHundredandOne.F2[j];
                            tempdata[3] = msgtwoHundredandOne.F3[j];
                            tempdata[4] = msgtwoHundredandOne.F4[j];
                            tempdata[5] = msgtwoHundredandOne.F5[j];
                            tempdata[6] = msgtwoHundredandOne.F6[j];
                            tempdata[7] = msgtwoHundredandOne.F7[j];


                            this.m_outputString[j] = this.m_outputString[j] + (tempdata[Hi_Bit] << 8 | tempdata[Lo_Bit] & 0xFF).ToString() + '|';
                        }
                        else if (Message_ID == "205")
                        {
                            byte[] tempdata = new byte[8];
                            tempdata[0] = msgtwoHundredandFive.F0[j];
                            tempdata[1] = msgtwoHundredandFive.F1[j];
                            tempdata[2] = msgtwoHundredandFive.F2[j];
                            tempdata[3] = msgtwoHundredandFive.F3[j];
                            tempdata[4] = msgtwoHundredandFive.F4[j];
                            tempdata[5] = msgtwoHundredandFive.F5[j];
                            tempdata[6] = msgtwoHundredandFive.F6[j];
                            tempdata[7] = msgtwoHundredandFive.F7[j];


                            this.m_outputString[j] = this.m_outputString[j] + (tempdata[Hi_Bit] << 8 | tempdata[Lo_Bit] & 0xFF).ToString() + '|';
                        }
                        else if (Message_ID == "204")
                        {
                            byte[] tempdata = new byte[8];
                            tempdata[0] = msgtwoHundredFour.F0[j];
                            tempdata[1] = msgtwoHundredFour.F1[j];
                            tempdata[2] = msgtwoHundredFour.F2[j];
                            tempdata[3] = msgtwoHundredFour.F3[j];
                            tempdata[4] = msgtwoHundredFour.F4[j];
                            tempdata[5] = msgtwoHundredFour.F5[j];
                            tempdata[6] = msgtwoHundredFour.F6[j];
                            tempdata[7] = msgtwoHundredFour.F7[j];


                            this.m_outputString[j] = this.m_outputString[j] + (tempdata[Hi_Bit] << 8 | tempdata[Lo_Bit] & 0xFF).ToString() + '|';
                        }
                    }


                }

                if (Signals[DataSetIndices[i]].GetType().ToString() == str2)
                {
                    CAN_Signals.EightBitSignal temp = (CAN_Signals.EightBitSignal)Signals[DataSetIndices[i]];



                    Message_ID = temp.msgID;
                    Can_Frame = Convert.ToInt32(temp.CanFrame.Substring(1, 1));
                    Lo_Bit = 0;
                    Hi_Bit = 0;
                    iBit = 0;
                    iScale = temp.scale;
                    sName = temp.Name;
                    ValueNames[i] = temp.Name;
                    for (int j = 0; j < this.outputstringCount; j++)
                    {
                        if (Message_ID == "100")
                        {
                            byte[] tempdata = new byte[8];
                            tempdata[0] = msgoneHundred.F0[j];
                            tempdata[1] = msgoneHundred.F1[j];
                            tempdata[2] = msgoneHundred.F2[j];
                            tempdata[3] = msgoneHundred.F3[j];
                            tempdata[4] = msgoneHundred.F4[j];
                            tempdata[5] = msgoneHundred.F5[j];
                            tempdata[6] = msgoneHundred.F6[j];
                            tempdata[7] = msgoneHundred.F7[j];


                            this.m_outputString[j] = this.m_outputString[j] + tempdata[Can_Frame].ToString() + '|';
                        }
                        else if (Message_ID == "200")
                        {
                            byte[] tempdata = new byte[8];
                            tempdata[0] = msgtwoHundred.F0[j];
                            tempdata[1] = msgtwoHundred.F1[j];
                            tempdata[2] = msgtwoHundred.F2[j];
                            tempdata[3] = msgtwoHundred.F3[j];
                            tempdata[4] = msgtwoHundred.F4[j];
                            tempdata[5] = msgtwoHundred.F5[j];
                            tempdata[6] = msgtwoHundred.F6[j];
                            tempdata[7] = msgtwoHundred.F7[j];


                            this.m_outputString[j] = this.m_outputString[j] + tempdata[Can_Frame].ToString() + '|';
                        }
                        else if (Message_ID == "500")
                        {
                            byte[] tempdata = new byte[8];
                            tempdata[0] = msgfiveHundred.F0[j];
                            tempdata[1] = msgfiveHundred.F1[j];
                            tempdata[2] = msgfiveHundred.F2[j];
                            tempdata[3] = msgfiveHundred.F3[j];
                            tempdata[4] = msgfiveHundred.F4[j];
                            tempdata[5] = msgfiveHundred.F5[j];
                            tempdata[6] = msgfiveHundred.F6[j];
                            tempdata[7] = msgfiveHundred.F7[j];


                            this.m_outputString[j] = this.m_outputString[j] + tempdata[Can_Frame].ToString() + '|';
                        }
                        else if (Message_ID == "206")
                        {
                            byte[] tempdata = new byte[8];
                            tempdata[0] = msgtwoHundredandSix.F0[j];
                            tempdata[1] = msgtwoHundredandSix.F1[j];
                            tempdata[2] = msgtwoHundredandSix.F2[j];
                            tempdata[3] = msgtwoHundredandSix.F3[j];
                            tempdata[4] = msgtwoHundredandSix.F4[j];
                            tempdata[5] = msgtwoHundredandSix.F5[j];
                            tempdata[6] = msgtwoHundredandSix.F6[j];
                            tempdata[7] = msgtwoHundredandSix.F7[j];


                            this.m_outputString[j] = this.m_outputString[j] + tempdata[Can_Frame].ToString() + '|';
                        }
                        else if (Message_ID == "207")
                        {
                            byte[] tempdata = new byte[8];
                            tempdata[0] = msgttwuHundredandSeven.F0[j];
                            tempdata[1] = msgttwuHundredandSeven.F1[j];
                            tempdata[2] = msgttwuHundredandSeven.F2[j];
                            tempdata[3] = msgttwuHundredandSeven.F3[j];
                            tempdata[4] = msgttwuHundredandSeven.F4[j];
                            tempdata[5] = msgttwuHundredandSeven.F5[j];
                            tempdata[6] = msgttwuHundredandSeven.F6[j];
                            tempdata[7] = msgttwuHundredandSeven.F7[j];


                            this.m_outputString[j] = this.m_outputString[j] + tempdata[Can_Frame].ToString() + '|';
                        }
                        else if (Message_ID == "202")
                        {
                            byte[] tempdata = new byte[8];
                            tempdata[0] = msgtwoHundredandtwo.F0[j];
                            tempdata[1] = msgtwoHundredandtwo.F1[j];
                            tempdata[2] = msgtwoHundredandtwo.F2[j];
                            tempdata[3] = msgtwoHundredandtwo.F3[j];
                            tempdata[4] = msgtwoHundredandtwo.F4[j];
                            tempdata[5] = msgtwoHundredandtwo.F5[j];
                            tempdata[6] = msgtwoHundredandtwo.F6[j];
                            tempdata[7] = msgtwoHundredandtwo.F7[j];


                            this.m_outputString[j] = this.m_outputString[j] + tempdata[Can_Frame].ToString() + '|';
                        }
                        else if (Message_ID == "203")
                        {
                            byte[] tempdata = new byte[8];
                            tempdata[0] = msgtwoHundredandThree.F0[j];
                            tempdata[1] = msgtwoHundredandThree.F1[j];
                            tempdata[2] = msgtwoHundredandThree.F2[j];
                            tempdata[3] = msgtwoHundredandThree.F3[j];
                            tempdata[4] = msgtwoHundredandThree.F4[j];
                            tempdata[5] = msgtwoHundredandThree.F5[j];
                            tempdata[6] = msgtwoHundredandThree.F6[j];
                            tempdata[7] = msgtwoHundredandThree.F7[j];


                            this.m_outputString[j] = this.m_outputString[j] + tempdata[Can_Frame].ToString() + '|';
                        }
                        else if (Message_ID == "208")
                        {
                            byte[] tempdata = new byte[8];
                            tempdata[0] = msgtwoHundredandEight.F0[j];
                            tempdata[1] = msgtwoHundredandEight.F1[j];
                            tempdata[2] = msgtwoHundredandEight.F2[j];
                            tempdata[3] = msgtwoHundredandEight.F3[j];
                            tempdata[4] = msgtwoHundredandEight.F4[j];
                            tempdata[5] = msgtwoHundredandEight.F5[j];
                            tempdata[6] = msgtwoHundredandEight.F6[j];
                            tempdata[7] = msgtwoHundredandEight.F7[j];


                            this.m_outputString[j] = this.m_outputString[j] + tempdata[Can_Frame].ToString() + '|';
                        }
                        else if (Message_ID == "201")
                        {
                            byte[] tempdata = new byte[8];
                            tempdata[0] = msgtwoHundredandOne.F0[j];
                            tempdata[1] = msgtwoHundredandOne.F1[j];
                            tempdata[2] = msgtwoHundredandOne.F2[j];
                            tempdata[3] = msgtwoHundredandOne.F3[j];
                            tempdata[4] = msgtwoHundredandOne.F4[j];
                            tempdata[5] = msgtwoHundredandOne.F5[j];
                            tempdata[6] = msgtwoHundredandOne.F6[j];
                            tempdata[7] = msgtwoHundredandOne.F7[j];


                            this.m_outputString[j] = this.m_outputString[j] + tempdata[Can_Frame].ToString() + '|';
                        }
                        else if (Message_ID == "205")
                        {
                            byte[] tempdata = new byte[8];
                            tempdata[0] = msgtwoHundredandFive.F0[j];
                            tempdata[1] = msgtwoHundredandFive.F1[j];
                            tempdata[2] = msgtwoHundredandFive.F2[j];
                            tempdata[3] = msgtwoHundredandFive.F3[j];
                            tempdata[4] = msgtwoHundredandFive.F4[j];
                            tempdata[5] = msgtwoHundredandFive.F5[j];
                            tempdata[6] = msgtwoHundredandFive.F6[j];
                            tempdata[7] = msgtwoHundredandFive.F7[j];


                            this.m_outputString[j] = this.m_outputString[j] + tempdata[Can_Frame].ToString() + '|';
                        }
                        else if (Message_ID == "204")
                        {
                            byte[] tempdata = new byte[8];
                            tempdata[0] = msgtwoHundredFour.F0[j];
                            tempdata[1] = msgtwoHundredFour.F1[j];
                            tempdata[2] = msgtwoHundredFour.F2[j];
                            tempdata[3] = msgtwoHundredFour.F3[j];
                            tempdata[4] = msgtwoHundredFour.F4[j];
                            tempdata[5] = msgtwoHundredFour.F5[j];
                            tempdata[6] = msgtwoHundredFour.F6[j];
                            tempdata[7] = msgtwoHundredFour.F7[j];


                            this.m_outputString[j] = this.m_outputString[j] + tempdata[Can_Frame].ToString() + '|';
                        }
                    }
                }
                if (Signals[DataSetIndices[i]].GetType().ToString() == str3)
                {
                    CAN_Signals.BoolSignal temp = (CAN_Signals.BoolSignal)Signals[DataSetIndices[i]];


                    Message_ID = temp.msgID;
                    Can_Frame = Convert.ToInt32(temp.canFrame.Substring(1, 1));
                    Lo_Bit = 0;
                    Hi_Bit = 0;
                    iBit = temp.bitNo;
                    iScale = 2;
                    sName = temp.Name;
                    ValueNames[i] = temp.Name;
                    ConvertToBinaryString.ConvertToBinaryString bs = new ConvertToBinaryString.ConvertToBinaryString();
                    for (int j = 0; j < this.outputstringCount; j++)
                    {
                        if (Message_ID == "100")
                        {
                            byte[] tempdata = new byte[8];
                            tempdata[0] = msgoneHundred.F0[j];
                            tempdata[1] = msgoneHundred.F1[j];
                            tempdata[2] = msgoneHundred.F2[j];
                            tempdata[3] = msgoneHundred.F3[j];
                            tempdata[4] = msgoneHundred.F4[j];
                            tempdata[5] = msgoneHundred.F5[j];
                            tempdata[6] = msgoneHundred.F6[j];
                            tempdata[7] = msgoneHundred.F7[j];

                            bs.IntValue = tempdata[Can_Frame];
                            string strData = bs.BinaryString;
                            this.m_outputString[j] = this.m_outputString[j] + strData.Substring(7 - iBit, 1) + '|';
                        }
                        else if (Message_ID == "200")
                        {
                            byte[] tempdata = new byte[8];
                            tempdata[0] = msgtwoHundred.F0[j];
                            tempdata[1] = msgtwoHundred.F1[j];
                            tempdata[2] = msgtwoHundred.F2[j];
                            tempdata[3] = msgtwoHundred.F3[j];
                            tempdata[4] = msgtwoHundred.F4[j];
                            tempdata[5] = msgtwoHundred.F5[j];
                            tempdata[6] = msgtwoHundred.F6[j];
                            tempdata[7] = msgtwoHundred.F7[j];

                            bs.IntValue = tempdata[Can_Frame];
                            string strData = bs.BinaryString;
                            this.m_outputString[j] = this.m_outputString[j] + strData.Substring(7 - iBit, 1) + '|';

                        }
                        else if (Message_ID == "500")
                        {
                            byte[] tempdata = new byte[8];
                            tempdata[0] = msgfiveHundred.F0[j];
                            tempdata[1] = msgfiveHundred.F1[j];
                            tempdata[2] = msgfiveHundred.F2[j];
                            tempdata[3] = msgfiveHundred.F3[j];
                            tempdata[4] = msgfiveHundred.F4[j];
                            tempdata[5] = msgfiveHundred.F5[j];
                            tempdata[6] = msgfiveHundred.F6[j];
                            tempdata[7] = msgfiveHundred.F7[j];

                            bs.IntValue = tempdata[Can_Frame];
                            string strData = bs.BinaryString;
                            this.m_outputString[j] = this.m_outputString[j] + strData.Substring(7 - iBit, 1) + '|';
                            //this.m_outputString[j] = this.m_outputString[j] + strData + '|';
                        }
                        else if (Message_ID == "206")
                        {
                            byte[] tempdata = new byte[8];
                            tempdata[0] = msgtwoHundredandSix.F0[j];
                            tempdata[1] = msgtwoHundredandSix.F1[j];
                            tempdata[2] = msgtwoHundredandSix.F2[j];
                            tempdata[3] = msgtwoHundredandSix.F3[j];
                            tempdata[4] = msgtwoHundredandSix.F4[j];
                            tempdata[5] = msgtwoHundredandSix.F5[j];
                            tempdata[6] = msgtwoHundredandSix.F6[j];
                            tempdata[7] = msgtwoHundredandSix.F7[j];

                            bs.IntValue = tempdata[Can_Frame];
                            string strData = bs.BinaryString;
                            this.m_outputString[j] = this.m_outputString[j] + strData.Substring(7 - iBit, 1) + '|';
                            //this.m_outputString[j] = this.m_outputString[j] + strData + '|';
                        }
                        else if (Message_ID == "207")
                        {
                            byte[] tempdata = new byte[8];
                            tempdata[0] = msgttwuHundredandSeven.F0[j];
                            tempdata[1] = msgttwuHundredandSeven.F1[j];
                            tempdata[2] = msgttwuHundredandSeven.F2[j];
                            tempdata[3] = msgttwuHundredandSeven.F3[j];
                            tempdata[4] = msgttwuHundredandSeven.F4[j];
                            tempdata[5] = msgttwuHundredandSeven.F5[j];
                            tempdata[6] = msgttwuHundredandSeven.F6[j];
                            tempdata[7] = msgttwuHundredandSeven.F7[j];

                            bs.IntValue = tempdata[Can_Frame];
                            string strData = bs.BinaryString;
                            this.m_outputString[j] = this.m_outputString[j] + strData.Substring(7 - iBit, 1) + '|';
                            //this.m_outputString[j] = this.m_outputString[j] + strData + '|';
                        }
                        else if (Message_ID == "202")
                        {
                            byte[] tempdata = new byte[8];
                            tempdata[0] = msgtwoHundredandtwo.F0[j];
                            tempdata[1] = msgtwoHundredandtwo.F1[j];
                            tempdata[2] = msgtwoHundredandtwo.F2[j];
                            tempdata[3] = msgtwoHundredandtwo.F3[j];
                            tempdata[4] = msgtwoHundredandtwo.F4[j];
                            tempdata[5] = msgtwoHundredandtwo.F5[j];
                            tempdata[6] = msgtwoHundredandtwo.F6[j];
                            tempdata[7] = msgtwoHundredandtwo.F7[j];

                            bs.IntValue = tempdata[Can_Frame];
                            string strData = bs.BinaryString;
                            this.m_outputString[j] = this.m_outputString[j] + strData.Substring(7 - iBit, 1) + '|';
                            //this.m_outputString[j] = this.m_outputString[j] + strData + '|';
                        }
                        else if (Message_ID == "203")
                        {
                            byte[] tempdata = new byte[8];
                            tempdata[0] = msgtwoHundredandThree.F0[j];
                            tempdata[1] = msgtwoHundredandThree.F1[j];
                            tempdata[2] = msgtwoHundredandThree.F2[j];
                            tempdata[3] = msgtwoHundredandThree.F3[j];
                            tempdata[4] = msgtwoHundredandThree.F4[j];
                            tempdata[5] = msgtwoHundredandThree.F5[j];
                            tempdata[6] = msgtwoHundredandThree.F6[j];
                            tempdata[7] = msgtwoHundredandThree.F7[j];

                            bs.IntValue = tempdata[Can_Frame];
                            string strData = bs.BinaryString;
                            this.m_outputString[j] = this.m_outputString[j] + strData.Substring(7 - iBit, 1) + '|';
                            //this.m_outputString[j] = this.m_outputString[j] + strData + '|';
                        }
                        else if (Message_ID == "208")
                        {
                            byte[] tempdata = new byte[8];
                            tempdata[0] = msgtwoHundredandEight.F0[j];
                            tempdata[1] = msgtwoHundredandEight.F1[j];
                            tempdata[2] = msgtwoHundredandEight.F2[j];
                            tempdata[3] = msgtwoHundredandEight.F3[j];
                            tempdata[4] = msgtwoHundredandEight.F4[j];
                            tempdata[5] = msgtwoHundredandEight.F5[j];
                            tempdata[6] = msgtwoHundredandEight.F6[j];
                            tempdata[7] = msgtwoHundredandEight.F7[j];

                            bs.IntValue = tempdata[Can_Frame];
                            string strData = bs.BinaryString;
                            this.m_outputString[j] = this.m_outputString[j] + strData.Substring(7 - iBit, 1) + '|';
                            //this.m_outputString[j] = this.m_outputString[j] + strData + '|';
                        }
                        else if (Message_ID == "201")
                        {
                            byte[] tempdata = new byte[8];
                            tempdata[0] = msgtwoHundredandOne.F0[j];
                            tempdata[1] = msgtwoHundredandOne.F1[j];
                            tempdata[2] = msgtwoHundredandOne.F2[j];
                            tempdata[3] = msgtwoHundredandOne.F3[j];
                            tempdata[4] = msgtwoHundredandOne.F4[j];
                            tempdata[5] = msgtwoHundredandOne.F5[j];
                            tempdata[6] = msgtwoHundredandOne.F6[j];
                            tempdata[7] = msgtwoHundredandOne.F7[j];

                            bs.IntValue = tempdata[Can_Frame];
                            string strData = bs.BinaryString;
                            this.m_outputString[j] = this.m_outputString[j] + strData.Substring(7 - iBit, 1) + '|';
                            //this.m_outputString[j] = this.m_outputString[j] + strData + '|';
                        }
                        else if (Message_ID == "205")
                        {
                            byte[] tempdata = new byte[8];
                            tempdata[0] = msgtwoHundredandFive.F0[j];
                            tempdata[1] = msgtwoHundredandFive.F1[j];
                            tempdata[2] = msgtwoHundredandFive.F2[j];
                            tempdata[3] = msgtwoHundredandFive.F3[j];
                            tempdata[4] = msgtwoHundredandFive.F4[j];
                            tempdata[5] = msgtwoHundredandFive.F5[j];
                            tempdata[6] = msgtwoHundredandFive.F6[j];
                            tempdata[7] = msgtwoHundredandFive.F7[j];

                            bs.IntValue = tempdata[Can_Frame];
                            string strData = bs.BinaryString;
                            this.m_outputString[j] = this.m_outputString[j] + strData.Substring(7 - iBit, 1) + '|';

                        }
                        else if (Message_ID == "204")
                        {
                            byte[] tempdata = new byte[8];
                            tempdata[0] = msgtwoHundredFour.F0[j];
                            tempdata[1] = msgtwoHundredFour.F1[j];
                            tempdata[2] = msgtwoHundredFour.F2[j];
                            tempdata[3] = msgtwoHundredFour.F3[j];
                            tempdata[4] = msgtwoHundredFour.F4[j];
                            tempdata[5] = msgtwoHundredFour.F5[j];
                            tempdata[6] = msgtwoHundredFour.F6[j];
                            tempdata[7] = msgtwoHundredFour.F7[j];

                            bs.IntValue = tempdata[Can_Frame];
                            string strData = bs.BinaryString;
                            this.m_outputString[j] = this.m_outputString[j] + strData.Substring(7 - iBit, 1) + '|';

                        }
                    }
                }



            }
        }


    }
}

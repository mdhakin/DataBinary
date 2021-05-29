
/* 
   By Matthew D Hakin
   May 27 2021

   This Class is responsible for loading the contents of a data file *****
    
   Order of events

    - Instantiate class
    - the loadFile() function is called and the file is loaded into memoory.
        - The loadFile() function reads a file in formatted in binary format.
            The file is a contigious binary file. Each record is 14 bytes long
            The first four bytes in an unsigned 32 bit number representing the 
            Unix Epoc timestamp of the record.
            The next 2 bytes are an unsigned 16 bit number representing the can message id of 
            that record.
            The next eight bytes represent the 8 can frames of the message. They are each an unsigned 8
            bit number.
        - If the loadFile() function is successful, the data is now avaialable for reading. If not successful, the 
            Object is dead.
 */
using System;
using System.IO;

namespace DataBinary
{
    public class RawFile : IRawFile
    {
        /// <summary>
        /// Error String
        /// </summary>
        public string FileError { get => m_error; }
        private string m_error;

        /// <summary>
        /// Returns the timestap of the specified record. Record is specified by passing
        /// in the index of the desired record
        /// </summary>
        public uint[] Msgtime { get => m_msgtime; }
        private UInt32[] m_msgtime;

        /// <summary>
        /// Returns the message id 
        /// </summary>
        public ushort[] Msgid { get => m_msgid; }
        private UInt16[] m_msgid;

        /// <summary>
        /// returns F0
        /// </summary>
        public byte[] F0 { get => m_f0; }
        private byte[] m_f0;

        /// <summary>
        /// returns F1
        /// </summary>
        public byte[] F1 { get => m_f1; }
        private byte[] m_f1;

        /// <summary>
        /// returns F2
        /// </summary>
        public byte[] F2 { get => m_f2; }
        private byte[] m_f2;

        /// <summary>
        /// returns F3
        /// </summary>
        public byte[] F3 { get => m_f3; }
        private byte[] m_f3;

        /// <summary>
        /// returns F4
        /// </summary>
        public byte[] F4 { get => m_f4; }
        private byte[] m_f4;

        /// <summary>
        /// returns F5
        /// </summary>
        public byte[] F5 { get => m_f5; }
        private byte[] m_f5;

        /// <summary>
        /// returns F6
        /// </summary>
        public byte[] F6 { get => m_f6; }
        private byte[] m_f6;

        /// <summary>
        /// returns F7
        /// </summary>
        public byte[] F7 { get => m_f7; }
        private byte[] m_f7;


        /// <summary>
        /// Holds the first date that occurs in this file
        /// </summary>
        public string FileStartDate { get => m_fileStartDate; }
        private string m_fileStartDate;


        /// <summary>
        /// Holds the last date in the file
        /// </summary>
        public string FileEnddate { get => m_fileEndDate; }
        private string m_fileEndDate;

        /// <summary>
        /// Calculated total time of this log.
        /// </summary>
        public int TotalHours { get => m_totalHours; }
        private int m_totalHours;

        /// <summary>
        /// The fullpath of the data file
        /// </summary>
        public string Filename { get => m_filename; }
        private string m_filename;

        /// <summary>
        /// The count of the number of records in the file
        /// </summary>
        public long RecordCount { get => m_Count; }
        private long m_Count;

        /// <summary>
        /// Indicates file load status
        /// </summary>
        public bool FileReady { get => fileready; }
        private bool fileready = false;


        /// <summary>
        /// init class
        /// </summary>
        /// <param name="filen">The full path of the data file</param>
        public RawFile(string filen)
        {
            if (File.Exists(filen))
            {
                this.m_totalHours = 0;
                this.m_filename = filen;
                loadFile();
            }
            else
            {
                this.m_Count = 0;
                this.fileready = false;
                this.m_error = "File Does Not Exist!";

            }

        }

        /// <summary>
        /// This is an internal function used to load the inital file into the class.
        /// </summary>
        /// <returns>If this returns anything but 0 there was an error.</returns>
        private int loadFile()
        {
            FileStream fs = new FileStream(this.m_filename, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(fs);

            // Calculate how many records there are
            long loops = fs.Length / 14;

            // Initialize the arrays according to the number of records
            this.m_msgtime = new UInt32[loops];
            this.m_msgid = new UInt16[loops];
            this.m_f0 = new byte[loops];
            this.m_f1 = new byte[loops];
            this.m_f2 = new byte[loops];
            this.m_f3 = new byte[loops];
            this.m_f4 = new byte[loops];
            this.m_f5 = new byte[loops];
            this.m_f6 = new byte[loops];
            this.m_f7 = new byte[loops];

            // hold the current timestamp to determine when a timestamp has changed
            UInt32 currTimeStamp = 0;

            // Increment every time a timestamp change occurs
            // at the end use to determine loggong frequency.
            long timestampCount = 0;

            if (loops > 0)
            {
                // Master loop through all records in the file.
                for (int i = 0; i < loops; i++)
                {
                    this.m_msgtime[i] = reader.ReadUInt32();
                    if (currTimeStamp != this.m_msgtime[i])
                    {
                        if (currTimeStamp == 0)
                        {
                            DateTime dt = new DateTime();
                            currTimeStamp = this.m_msgtime[i];
                            this.m_fileStartDate = currTimeStamp.ToString();
                            timestampCount++;
                        }
                        else
                        {
                            timestampCount++;
                            if (this.m_msgtime[i] - currTimeStamp < 30)
                            {
                                this.m_totalHours = Convert.ToInt32(this.m_totalHours + (this.m_msgtime[i] - currTimeStamp));
                                currTimeStamp = this.m_msgtime[i];
                            }
                            else
                            {
                                // To Do Calculate total hours
                                currTimeStamp = this.m_msgtime[i];
                            }
                        }
                    }
                    if (this.m_msgtime[i] < 1500000000)
                    {
                        this.m_Count = 0;
                        this.fileready = false;
                        this.m_error = "Data not formatted Correctly";
                        return -1;
                    }
                    this.m_msgid[i] = reader.ReadUInt16();
                    this.m_f0[i] = reader.ReadByte();
                    this.m_f1[i] = reader.ReadByte();
                    this.m_f2[i] = reader.ReadByte();
                    this.m_f3[i] = reader.ReadByte();
                    this.m_f4[i] = reader.ReadByte();
                    this.m_f5[i] = reader.ReadByte();
                    this.m_f6[i] = reader.ReadByte();
                    this.m_f7[i] = reader.ReadByte();
                }

                // set the total number of records in this file
                this.m_Count = loops;

                // Set the start and end Dates
                this.m_fileStartDate = UT.UnixTimeStampToDateTime(Convert.ToDouble(this.m_msgtime[0])).ToString();
                this.m_fileEndDate = UT.UnixTimeStampToDateTime(Convert.ToDouble(this.m_msgtime[this.m_Count - 1])).ToString();
            }
            else
            {
                this.m_error = "Error loading data";
                this.fileready = false;
                return -1;
            }

            // Close the reader
            reader.Close();
            // Close the file
            fs.Close();

            // Indicate that the file is loaded and ready for access
            this.fileready = true;
            return 0;
        }

        /// <summary>
        /// Error property. Is filled when something goes wrong.
        /// </summary>
        /// <returns></returns>
        public string Error()
        {
            return this.m_error;
        }

        /// <summary>
        /// Returns the size of the file in bytes.
        /// </summary>
        /// <returns></returns>
        public long sizeInBytes()
        {
            if (this.m_Count > 0)
            {
                return this.m_Count * 14;
            }
            else
            {
                return 0;
            }
        }

        public long checkData()
        {

            long out1 = sizeInBytes() % 11;

            return out1;
        }
    }
}

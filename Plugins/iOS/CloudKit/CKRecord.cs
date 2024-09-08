using System;
using System.Runtime.InteropServices;
using CKRecordType = System.String;

namespace iOSNativePlugin.CloudKit
{
    public class CKRecord
    {
        readonly IntPtr handle;
        [DllImport("__Internal")] static extern void ReleaseCKRecord(IntPtr handle);
        
        ~CKRecord()
        {
            ReleaseCKRecord(handle);
        }
        
        /// <summary>
        /// - (instancetype)initWithRecordType:(CKRecordType)recordType;
        /// <para>Creates a new record of the specified type.</para>
        /// </summary>
        /// <param name="recordType">A string that represents the type of record that you want to create. You can’t change the record type after initialization. You define the record types that your app supports and use them to distinguish between records with different types of data. This parameter must not be nil or contain an empty string.
        /// <para>A record type must consist of one or more alphanumeric characters and must start with a letter. CloudKit permits the use of underscores, but not spaces.</para></param>
        /// <link>https://developer.apple.com/documentation/cloudkit/ckrecord/1462225-initwithrecordtype?language=objc</link>
        public CKRecord(CKRecordType recordType)
        {
            handle = InitCKRecord(recordType);
        }
        [DllImport("__Internal")] static extern IntPtr InitCKRecord(CKRecordType recordType);
        
    }
}

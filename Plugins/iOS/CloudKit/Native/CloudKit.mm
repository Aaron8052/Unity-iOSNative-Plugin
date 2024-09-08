#import <CloudKit/CloudKit.h>

extern "C"
{

CKRecord* InitCKRecord(const char* recordType){
    CKRecord* record = [[CKRecord alloc] initWithRecordType:[NSString stringWithUTF8String:recordType]];
    return record; // 防止指针被回收
}

void ReleaseCKRecord(CKRecord* handle){

}
}

// MIT License
//
// Copyright(c) 2023 Mikan31
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

//
//  UniKeyChainAccess.m
//  UniKeyChainAccess
//
//  Created by Mikan31 on 2023/10/08.
//

#import "UnityFramework/UnityFramework-Swift.h"

#ifdef __cplusplus
extern "C" {
#endif

/**
 * Save
 */
bool Save(const char* key, const char* data)
{
    NSLog(@"Save");
    NSString* keyStr = [NSString stringWithCString: key encoding:NSUTF8StringEncoding];
    NSString* dataStr = [NSString stringWithCString:data encoding:NSUTF8StringEncoding];
    return [UniKeyChainAccess Save:keyStr saveData:dataStr];
}

/**
 * Load
 */
const char* Load(const char* key)
{
    NSLog(@"Load");
    NSString* keyStr = [NSString stringWithCString: key encoding:NSUTF8StringEncoding];
    NSString* result = [UniKeyChainAccess Load:keyStr];
    return strdup([result UTF8String]);
}

/**
 * Delete
 */
bool Delete(const char* key)
{
    NSLog(@"Delete");
    NSString* keyStr = [NSString stringWithCString: key encoding:NSUTF8StringEncoding];
    return [UniKeyChainAccess Delete:keyStr];
}
    
#ifdef __cplusplus
}
#endif

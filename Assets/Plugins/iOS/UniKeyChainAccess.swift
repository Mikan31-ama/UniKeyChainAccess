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
//  UniKeyChainAccess.swift
//  UniKeyChainAccess
//
//  Created by Mikan31 on 2023/10/08.
//

import Foundation

/**
 * Uni Key Chain Access class
 */
@objc public class UniKeyChainAccess : NSObject
{
    /**
     * Save
     */
    @objc public static func Save(_ key:String, saveData:String) -> Bool
    {
        NSLog("Save : \(key) : \(saveData)")
        let query = [kSecValueData:saveData.data(using: .utf8)!,
                         kSecClass: kSecClassGenericPassword,
                   kSecAttrAccount: key] as CFDictionary
        
        let resultStatus = SecItemCopyMatching(query, nil)
        var result:Bool = false;
        switch resultStatus
        {
            case errSecItemNotFound:
                print("ErrSecItemNotFound save data from key : " + key)
                let secItemAddStatus = SecItemAdd(query, nil)
                print("Save SecItemAdd result : \(SecCopyErrorMessageString(secItemAddStatus, nil))")
                result = secItemAddStatus == noErr;
            case errSecSuccess:
                print("Save data done from key : " + key)
                let secItemUpdateStatus = SecItemUpdate(query, [kSecValueData as String: saveData.data(using: .utf8)!] as CFDictionary)
                print("Save SecItemUpdate result :  \(SecCopyErrorMessageString(secItemUpdateStatus, nil))")
                result = secItemUpdateStatus == noErr
            default:
                print("Save data can not key : \(key) / SecItemCopyMatching result : \(SecCopyErrorMessageString(resultStatus, nil))")
                result = false
        }
        return result;
    }
    
    /**
     * Load
     */
    @objc public static func Load(_ key:String) -> String
    {
        let query = [kSecClass: kSecClassGenericPassword,
                   kSecAttrAccount: key,
                kSecReturnData:true] as CFDictionary
        var result:AnyObject?
        let resultStatus = SecItemCopyMatching(query, &result)
        print("Load SecItemCopyMatching result : \(SecCopyErrorMessageString(resultStatus, nil))")
        var value:String = ""
        if let data = result as? Data
        {
            value = String(data:data, encoding: .utf8) ?? ""
        }
        
        return value;
    }
    
    /**
     * Delete
     */
    @objc public static func Delete(_ key:String) -> Bool
    {
        let query = [kSecClass: kSecClassGenericPassword, kSecAttrAccount: key] as CFDictionary
        let resultStatus = SecItemDelete(query)
        print("Delete SecItemDelete result : \(SecCopyErrorMessageString(resultStatus, nil))")
        return resultStatus == noErr
    }
}

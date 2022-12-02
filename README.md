# Cab Booking App

        *   Asp .net Core 7.0 MVC
        *   Microsoft SQL Server
        *   BootsWatch Css ( Modified Bootstrap Css ) 
        *   Bootstrap 5
        *   java script

---

### Users

- **Administrator**
- **Driver**
- **Customer**

## Existing Functionalities

### Driver

> 1. **Driver Can Register To Use The App Providing**
>> `First Name`, `Last Name`, `Email Id`, `Password`
>
>> **Scope ✅ | Out of Scope ❌**
>>> *server side validation are done for all fields*
>>>> - *mail Id must be unique* ✅
>>>>  - *all fields must not be null* ✅
>>> > - *Password should contain `uppercase`, `lowercase`,`special characters` & `numbers`* ✅
>>> > - *email verification* ❌
>>> > - *First and last Name should only contain letters* ❌
>>> > - *Client side validation* ❌
>
>> **Success Scenario**
>>> *redirected to sign in page*
>
>> **Failed Action**
>>> *redirected to Registration Page*
>
> 2. **Driver Can Sign In Providing**
>> **`email` , `password`**
>
>> **Scope ✅ | Out of Scope ❌**
>>> - *a user with the given email exists or not* ✅
>>> - *password verification* ✅
>>> - *two factor authentication* ❌
>>> - *forgot password* ❌
>>> - *account recovery options* ❌
>
>> **Success Scenario**
>>> *redirected Drivers Home Page*
>
>> **Failed Action**
>>> *redirected to Sign In Page*
> 3. **First Time Drivers Should Complete Additional Verification Step, Drivers Have To Provide**
>> * `Phone Number`, `Licence Number`, `Vehicle Regeistration Number`
>> * `Address Details`, `Aadhar Number`, `PAN Number`,`cab type`
>
>> **Scope ✅ | Out of Scope ❌**
>>> - *all fields should not be empty* ✅
>>> - *phone number should only contain numbers* ✅
>>> - *aadhar number should only contain numbers* ✅
>>> - *Postal code should only contain numbers* ✅
>>> - *phone number should be 10 digits* ❌ ( *will be included* )
>>> - *Postal Code number should be 12 digits* ❌ ( *will be included* )
>>> - *PIN number should be 6 digits* ❌ ( *will be included* )
>>> - *phone number OTP verification* ❌
>>> - *Aadhar number OTP Verification* ❌
>>> - *PAN number Verification* ❌
>>> - *Photo upload* ❌
>>> - *Additional Document Uploads*    ❌
      >>>
- `RC Details`, `Aadhar`, `PAN Card`, `Driver's Licence`,`Pollution Certificate`,
>>>   - `vehicle fitness certificate`, `Police Clearance Certificate`, `Address proof`,
>>> - *Temporary Suspension of Drivers account upon Expiration of Any of the above mentioned Documents* ❌
>
>> **Success Scenario**
>>> - *Details are forwarded to Admin for Verification and Approval*
>>> - *Driver Is Redirected To His Profile Page*
>>> - *Account Status is Maintained as Pending*

> > **Failed Action**
>>> - *redirected To Same Page*
>>> - *All other Account Features Are Locked*
>
> 4. **A Verified Driver Can Access Profile Page**
>> **Scope ✅ | Out of Scope ❌**
>>> - *Update Email* ✅
>>> - *Update his/her Personal Details* `✅`
>>> - *Change His/her first name and last name* `✅`
>>> - *change phone number* ✅
>>> - *Select A preferred Work location* ✅
>>> - *change his preferred Work location* ✅
>>> - *choose weather to accept bookings or not* ✅
>>> - *checks email is associated with any other accounts on email change* ✅
>>> - *email OTP verification* ❌
>>> - *checks phone number is associated with any other accounts on email change* ❌
>>> - *phone number OTP verification* ❌

> 5. **A Verified Driver Can Access Bookings Page**
>> **Scope ✅ | Out of Scope ❌**
>>>

> 6. **A Verified Driver Can View His Dashboard**
>
> 7. **A Driver Can Logout**
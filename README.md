csharp-antispam
===============

CleanTalk service API for C#. It is invisible protection from spam, no captchas, no puzzles, no animals, and no math.

## Actual API documentation

* [check_message](https://cleantalk.org/help/api-check-message) - Check IPs, Emails and messages for spam activity
* [check_newuser](https://cleantalk.org/help/api-check-newuser) - Check registrations of new users
* [spam_check](https://cleantalk.org/help/api-spam-check) - This method should be used for bulk checks of IP, Email for spam activity
* [ip_info](https://cleantalk.org/help/api-ip-info-country-code) - method returns a 2-letter country code (US, UK, CN, etc.) for an IP address

## How does the API stop spam?

The API uses several simple tests to stop spammers.
* Spambot signatures.
* Blacklist checks by Email, IP, website domain names.
* Javascript availability.
* Comment submit time.
* Relevance test for the comment.

## How does the API work?

API sends the comment's text and several previous approved comments to the server. The server evaluates the relevance of the comment's text on the topic, tests for spam and finally provides a solution - to publish or to put in manual moderation queue of comments. If a comment is placed in manual moderation queue, the plugin adds a rejection explanation to the text of the comment.

## Requirements

* [.Net Framework v4.8](https://dotnet.microsoft.com/download/dotnet-framework)
* CleanTalk account https://cleantalk.org/register?product=anti-spam


## SPAM test examples

### Usage of check_message method for text comment verify

```c#

public const string AuthKey = "auth key";

//...
var req1 = new CleantalkRequest(AuthKey)
{
    Message = "This is a great storm!",
    SenderInfo = new SenderInfo
    {
        Refferrer = "https://www.bbc.co.uk/sport",
        UserAgent = "Opera/9.80 (Windows NT 6.1; WOW64) Presto/2.12.388 Version/12.12"
    },
    SenderIp = "91.207.4.192",
    SenderEmail = "keanu8dh@gmail.com",
    SenderNickname = "Mike",
    EventToken = "f32f32f32f32f32f32f32f32f32f32a2",
    ///     To get this param:
    ///         1. add a script to the web-page: <script src="https://moderate.cleantalk.org/ct-bot-detector-wrapper.js" id="ct_bot_detector-js"></script>
    ///         2. parse the newly added hidden input on the web form, the name atrribute of input is "ct_bot_detector_event_token" 
    ///     @var string
    SubmitTime = 15
};

var res1 = _cleantalk.CheckMessage(req1);

```
API returns response object:
  * allow (0|1) - allow to publish or not, in other words spam or ham.
  * comment (string) - server comment for requests.
  * id (string MD5 HEX hash) - unique request idenifier.
  * errno (int) - error number. errno will be 0 if request is successful.
  * errtstr (string) - comment explaining the error. errstr will be `null` if request is successful.
  

### Usage of spam_check method for bulk parameters

```c#
public const string SpamCheckAuthKey = "spam_check auth key";

//...
var req1 = new SpamCheckRequest(SpamCheckAuthKey)
{
    data = "stop_email@example.com,10.0.0.1,10.0.0.2"
};
var res1 = _cleantalk.SpamCheck(req1);
```
Result:
```json
res1={
  "data":
  {
    "stop_email@example.com":{
      "appears":true,
      "disposable_email":false,
      "sha256":"6d42ca0235d72b01a2b086ad53b5cfac24b5a444847fad70250e042d7ca8bf59",
      "spam_rate":0,
      "submitted":"2023-09-06 07:20:49",
      "updated":"2023-09-06 07:20:49"
      },
    "10.0.0.1":{
      "domains_count":1510,
      "frequency":2,
      "network_type":"hosting",
      "sha256":"f5047344122f0dee9974ba6761e61c6b8649e1f3968d13a635ebbf7be53a3a0d",
      "spam_rate":0,
      "submitted":"2020-06-19 09:57:46",
      "updated":"2023-11-01 18:58:11"
      },
    "10.0.0.2":{
      "domains_count":35,
      "frequency":1,
      "network_type":"hosting",
      "sha256":"cb5f37b4762871e6bbeccee663cb332438340c469160c634566ecc7c7e01009f",
      "spam_rate":0,
      "submitted":"2022-01-18 19:58:09",
      "updated":"2023-07-19 18:58:13"
      }
  }
}
```


### Usage of ip_info method for detecting county 2-letter code

```c#
public const string AuthKey = "auth key";

//...
var res1 = _cleantalk.IpInfoCheck(AuthKey, "8.8.8.8", "213.239.245.253", "109.191.240.212");
 
```
Result:
```json
res1={
  "data":
  {
    "8.8.8.8":{"country_code":"US","country_name":"United States"},
    "213.239.245.253":{"country_code":"DE","country_name":"Germany"},
    "109.191.240.212":{"country_code":"RU","country_name":"Russian Federation"}
  }
}
```
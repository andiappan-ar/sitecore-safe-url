# What is Sitecore safe url project

This project is a sample implementation of secure the Sitecore application URLS. Below security aspects will cover as part of below

* HTTP Security headers 
* Safe URL
* Google Recaptcha 
* Request parameter protection 
* Anti-forgery token validation

# Quick introduction

## HTTP Security headers 
HTTP security headers are a fundamental part of website security. Upon implementation, they protect you against the types of attacks that your site is most likely to come across. These headers protect against XSS, code injection, clickjacking, etc.

Configure the URL and Headers value as below in content management. Headers settings can be Grouped/Single, as well this settings can be append or overwritten as well.

Group settings refers common security headers which is shared by multiple sites.
![image](https://user-images.githubusercontent.com/11770345/126044632-d43e6eb9-81f8-45ca-991a-3f0feccf98c1.png)
![image](https://user-images.githubusercontent.com/11770345/126044677-9b56c8a7-61fb-4e26-9d2b-8601d4e6541e.png)

Domain specific settings refers to single specific domain security headers.
![image](https://user-images.githubusercontent.com/11770345/126044691-1f2521cb-1354-48d8-9e87-c84edbea4c15.png)


Result in sites as below,

* Group settings applied 
* Domain specific settings appended on top of group settings.

![image](https://user-images.githubusercontent.com/11770345/126044519-6beaa1bf-8372-441a-acbd-dc1e50119e4f.png)


## Safe URL
Secure the URL by allowed characters. If any disallowed characters present in URL , It will redirect to error page.

Configure the URLS , disallowed characters and error pages.
![image](https://user-images.githubusercontent.com/11770345/126044897-52ad1cd8-bc74-4db8-be60-ad330618c99f.png)

Result in action below,

* URL with allowed characters.
![image](https://user-images.githubusercontent.com/11770345/126044949-06f7f8e9-1bb1-488f-a88d-8c0d0c9acf4f.png)

* URL with disallowed characters.
![image](https://user-images.githubusercontent.com/11770345/126044960-e74ef831-0ba6-4899-b88f-7e2c0beb704f.png)

## Request parameter protection 
Protect the request parameters by Parameter filter attribute. 

Configure the parameter filter characters.
![image](https://user-images.githubusercontent.com/11770345/126045124-b8a3ded0-7845-4d4b-b0ac-f407cc834612.png)

Implement the filter attribute in respective model parameter. 
![image](https://user-images.githubusercontent.com/11770345/126045171-c43f0a2b-a2ac-4050-9825-50fc5394fb07.png)

Model validation will validate the configured parameter. As shown below,
![image](https://user-images.githubusercontent.com/11770345/126045192-1383ed43-8bcf-485e-9876-dcb9400dff69.png)

## Google Recaptcha 
Google recaptcha server side snippet will validate your google recaptcha response.

Initiate the google recaptcha server side validation as showned below, Result is boolean true|false.
![image](https://user-images.githubusercontent.com/11770345/126045412-5269262c-94f4-40d6-a116-e113302bb623.png)


##  Anti-forgery token</h4>


<!-- wp:paragraph -->
<p>This implementation is to prevent CSRF attacks and implement this as like in below article.</p>
<!-- /wp:paragraph -->

<!-- wp:paragraph -->
<p>Implement first in client side , which place you need. This will generate token.</p>
<!-- /wp:paragraph -->

<!-- wp:syntaxhighlighter/code {"language":"xml"} -->
<pre class="wp-block-syntaxhighlighter-code">&lt;!--Place this in your cshtml-->
@Html.AntiForgeryToken()
</pre>
<!-- /wp:syntaxhighlighter/code -->

<!-- wp:paragraph -->
<p>In your DOM it will create token like below.</p>
<!-- /wp:paragraph -->

<!-- wp:syntaxhighlighter/code {"language":"xml"} -->
<pre class="wp-block-syntaxhighlighter-code">&lt;!--Out put will be in your DOM-->
&lt;input name="__RequestVerificationToken" type="hidden" value="SKEfZKpGsh_YuUWuhhtFdoIhJS7doYRoaf7iIFIr4EGy1bQu9nB5KsdAgSz8VVDyrwjtyGtEW5QvJXJwtcBZ0L_CgzqjVg_UdLagcDDFBs41"></pre>
<!-- /wp:syntaxhighlighter/code -->

<!-- wp:paragraph -->
<p>Attach this token in request header. Where ever you are making API call.</p>
<!-- /wp:paragraph -->

<!-- wp:image {"id":384,"sizeSlug":"large","linkDestination":"none","className":"is-style-default"} -->
<figure class="wp-block-image size-large is-style-default"><img src="https://andisitecore.files.wordpress.com/2021/08/image-1.png?w=1024" alt="" class="wp-image-384"/></figure>
<!-- /wp:image -->

<!-- wp:paragraph -->
<p>In server side validate it using authorization filter and decorate on top of your API action method.</p>
<!-- /wp:paragraph -->

<!-- wp:paragraph {"style":{"color":{"background":"#d0f1dc"}}} -->
<p class="has-background" style="background-color:#d0f1dc">SRC :<a href="https://github.com/andiappan-ar/sitecore-safe-url/blob/main/Src/Module/SafeValidation/AntiforgeryTokenValidator.cs" target="_blank" rel="noreferrer noopener"> https://github.com/andiappan-ar/sitecore-safe-url/blob/main/Src/Module/SafeValidation/AntiforgeryTokenValidator.cs</a></p>
<!-- /wp:paragraph -->

<!-- wp:paragraph -->
<p><em>Authorization filter:</em></p>
<!-- /wp:paragraph -->

![image](https://user-images.githubusercontent.com/11770345/128060050-bfb57f02-b3dd-4591-9fb9-d932905d1335.png)


<!-- wp:paragraph -->
<p>Decorate like this <strong><a href="https://github.com/andiappan-ar/sitecore-safe-url/blob/main/Src/Controllers/SitecoreSafeController.cs" data-type="URL" data-id="https://github.com/andiappan-ar/sitecore-safe-url/blob/main/Src/Controllers/SitecoreSafeController.cs" target="_blank" rel="noreferrer noopener">[AntiforgeryTokenValidator]</a></strong> on top of your action method.</p>
<!-- /wp:paragraph -->

## Get started

This project is built on top of SItecore 10.0 , Please download the code and feel free to change as per your Sitecore version.

# Twitch flashbang
*Funny little app to torture streamers. Viewers donate, streamer go blind.*
**(WINDOWS ONLY!)**

![New Project](https://user-images.githubusercontent.com/80382462/197171306-2e924330-908e-4431-81da-a51b0ef804ae.png)

Made by me because I was bored and this sounded funny. My first REAL project :)

> #### ⚠️ PHOTOSENSITIVE EPILEPSY WARNING! ⚠️
> If you suffer from photosensitive epilepsy and are prone to seizures, You probably shouldn't use this app.
> 
> ***IF YOU ARE A STREAMER OR YOUTUBER THAT PLANS TO USE THIS IN FRONT OF AN AUDIENCE, PLEASE PUT A WARNING SO PEOPLE KNOW WHAT HELL THEY'RE ABOUT TO EXPIRIENCE!***

# Install, game settings and recording settings
This app can be downloaded on the releases page [here](https://github.com/Exilon24/TwitchFlashbang/releases). ~~I have yet to test recording and streaming the app so right now, you will have to figure it out yourself~~. To run this over games, the game will **have** to be set to borderless fullscreen as exclusive fullscreen does what its name suggests and overriding that is detectable (**and even bannable**) by most games. There is no exception to this rule :(

## Capturing on OBS

**To get started, make sure the flashbang overlay is active! Learn to do that [here](https://github.com/Exilon24/TwitchFlashbang/blob/master/README.md#usage)!**

To capture the flash on obs, add another `window capture` and add the flashbang `(null)` window to `winow capture` and set the capture method to `BitBlt (Windows 7 and up)`:

![image](https://user-images.githubusercontent.com/80382462/197275283-f734ca89-b15f-4af8-8e61-c2bfb67067c4.png)

If you did it correctly, your screen on OBS should go completely black... Don't worry though! I have a fix for this! Right click window capture you just made, and select `filters`:

![image](https://user-images.githubusercontent.com/80382462/197275502-5b588f7c-02a4-4cbf-b370-c18aaafbe320.png)

That should open up this window:

![image](https://user-images.githubusercontent.com/80382462/197275607-5e02c38f-3413-457e-ad84-f75da27397ff.png)

Now all you do is click the `+` icon at the bottom left, add a `luma key` filter and set the `Luma Max` and `Luma Min Smooth` options to `1.0`. **Set everything else to `0.0`**. If done right, your `luma key` window should look like mine: 

![image](https://user-images.githubusercontent.com/80382462/197275831-ec1a4db8-bbed-427f-b485-e8ca9e13eeea.png)

And voila! The overlay is now invisible untill the flashbang is triggered! Now it should overlay any game properly without you needing to use display capture!

> ### ❗NOTE: MAKE SURE TO PUT THE OVERLAY WINDOW CAPTURE SOURCE ABOVE EVERYTHING. IF YOU WANT YOUR CHAT AND SUBSCRIPTION MESSAGES TO STILL BE VISIBLE THROUGH THE FLASH, PUT THEM ABOVE LIKE SO:

![image](https://user-images.githubusercontent.com/80382462/197276247-06709a3f-6a68-43d3-95ab-fa1d161012d5.png)

# Usage
Upon opening the app (`TwitchFlashbang.exe`), you will be greeted by four different things:
* The donation handler (**WIP**). This is where you select what API you will be using to trigger the events in the app.
  - As of now, streamlabs is the only option.
* The SocketAPI token box. This is where you will put your [SocketAPI token](https://streamlabs.com/dashboard/settings/api-settings) (You must be logged into streamlabs to access your [SocketAPI token](https://streamlabs.com/dashboard/settings/api-settings)). The text box is censored so no one can see your token being entered incase you are setting this up on stream.
* The settings. This is where you select what events you want to trigger the flashbang.
* The `Ready` , `Test flash` and the flashbang queue.
  - The `Ready` button will enable the application and enable the overlay. **This means the flashbang is now active!**
  - The `Test flash` button is mainly used for debugging and checking how well the flashbang effect works on your machine. If you are expiriencing issues with the flashbang effect, please make an [issue](https://github.com/Exilon24/TwitchFlashbang/tree/v0.1.0#issues-bug-reports-and-suggestions) **as this app hasn't been tested with screen resolutions over 1920 x 1080**.
  - Finally, the app has a flashbang queueing system. This exists half as to not absouloutely violate the machine that the app is running on and half because its way easier to program as a bug failsafe. If an event is triggered whilst the flashbang is active, the queue will increase. **If the queue is not zero, you will get flashbanged, one after the other untill the queue is reduced to zero.**
![image](https://user-images.githubusercontent.com/80382462/197049811-6b5c74ca-8466-425d-903a-e0c398641af6.png)

## Connecting your account
In order to connect your account, first go to https://streamlabs.com/dashboard/settings/api-settings. You should be greeted by the following page. You will want to click on the `API tokens tab`:

![image](https://user-images.githubusercontent.com/80382462/197050651-1654cfdd-7849-45f0-a925-e561efdb190a.png)

Next, copy your `SocketAPI token`:

![image](https://user-images.githubusercontent.com/80382462/197050817-bdd87620-0e36-4120-aa6a-7f9b53cd2f01.png)

And paste it into the `SocketAPI token` box:

![image](https://user-images.githubusercontent.com/80382462/197050956-31edc84c-22cf-4bb9-bf69-3dd5c1b2901b.png)

_Make sure not to add spaces or any unnecessary characters_

Finally, Choose your desired events, select streamlabs as a donation provider and hit `Ready`:

![image](https://user-images.githubusercontent.com/80382462/197051439-c4cfc927-3877-4d4b-8eed-219bf75a8950.png)

> ### ❗NOTE: IF YOU DO NOT SEE THIS MESSAGE POP UP, YOU HAVE NOT BEEN CONNECTED TO STREAMLABS. RESTART THE APP AND FIGURE OUT WHAT YOU DID WRONG.❗

![image](https://user-images.githubusercontent.com/80382462/197051505-941ab0ed-dbec-468d-8410-a1a3bd5cfa43.png)

_You can also test the streamlabs connection with the test buttons on your [dashboard](https://streamlabs.com/dashboard#/alertbox). If you do not go blind, you did something wrong._

HAVE FUN GOING BLIND! 💥

# Privacy and security
This app requires a streamlabs `SocketAPI token` (**NOT TO BE MISTAKEN AS THE `access_token`**). Some of you may be concerned as to what this app can do to your streamlabs account through this token and you're correct for doing so. This token only gives the app access to read certain "events" that happen to your streamlabs account (Such as donations or subscriptions). If you want to know more about what data this app will take, look [here](https://dev.streamlabs.com/docs/socket-api). I also spoke directly to the streamlabs API support team and they clarify the safety of the token here: 

![image](https://user-images.githubusercontent.com/80382462/197052277-4ca52449-12d3-4af8-aa32-080b518d8a6b.png)
_(Of course, personal information has been censored)_

> ### NOTE: YOUR ANTIVIRUS MAY PREVENT THE APP FROM OPENING. I'M WORKING ON THIS RIGHT NOW BUT IN THE MEANTIME, YOU WILL HAVE TO WHITELIST THIS APP OR TURN ON YOUR ANTIVIRUS.

# Issues, Bug reports and suggestions
If you find a bug with the app, have a suggestion or just have a general issue with the app, please create a new issue on the [issues section](https://github.com/Exilon24/TwitchFlashbang/issues) with the appropriate tags. They will be reviewed, answered or resolved if they can. Please also provide evidence or examples to any issue or problem you may be having as it makes it infinitely easier to find and fix the issue.

> ### Please avoid posting known issues as they probably will be closed on post. Here are a list of known issues:
> * The flashbang overlay doesn't properly fit the screen
> * The app locked at 60 FPS.
>   - There might be an option to change this later but as of know, the flashbang effect is dependent on the framerate of the overlay. This is how the overlay works and I can't really do much about it **(Note: If the Win32 version is developed, this may not be a limitation)**. If you're streaming 
or recording, 60 FPS should already be fine for your viewers as usually videos and streams aren't recorded at more than 60 FPS. If you're using a 60hz monitor, the flashbang effect may look a little choppy but otherwise, its still fine.

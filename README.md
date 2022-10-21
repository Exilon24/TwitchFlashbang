# Twitch flashbang
*Funny little app to torture streamers. Viewers donate, streamer go blind.*
**(WINDOWS ONLY!)**

Made by me because I was bored and this sounded funny.

> #### ⚠️ PHOTOSENSITIVE EPILEPSY WARNING! ⚠️
> If you suffer from photosensitive epilepsy and are prone to seizures, You probably shouldn't use this app.
> 
> ***IF YOU ARE A STREAMER OR YOUTUBER THAT PLANS TO USE THIS IN FRONT OF AN AUDIENCE, PLEASE PUT A WARNING SO PEOPLE KNOW WHAT HELL THEY'RE ABOUT TO EXPIRIENCE!***

# Install, game settings and recording settings
This app can be downloaded on the releases page [here](https://github.com/Exilon24/TwitchFlashbang/releases). I have yet to test recording and streaming the app so right now, you will have to figure it out yourself. To run this over games, the game will have to be set to borderless fullscreen as exclusive fullscreen does what its name suggests and ovveriding that is bannable by most games. To capture the flash on obs, add another game capture and add the flashbang `(null)` window to the window to capture:

![image](https://user-images.githubusercontent.com/80382462/197054101-fa27d8b3-c936-4d47-818a-52f42086c9e5.png)

> ### NOTE: IF THIS DOESN'T WORK, YOU WILL HAVE TO RESORT TO USING DISPLAY CAPTURE.
> This is a limitation with the game overlay. This will not be a problem on the Win32 version if it is developed. 

# Usage
Upon opening the app (`TwitchFlashbang.exe`), you will be greeted by four different things:
* The donation handler (**WIP**). This is where you select what API you will be using to trigger the events.
  - As of now, streamlabs is the only option.
* The SocketAPI token box. This is where you will put your [SocketAPI token](https://streamlabs.com/dashboard/settings/api-settings) (You must be logged into streamlabs to access your [SocketAPI token](https://streamlabs.com/dashboard/settings/api-settings)). It is censored so no one can see your token being entered.
* The settings. This is where you select what events you want to trigger the flashbang.
* The `Ready` , `Test flash` and the flashbang queue.
  - The `Ready` button will enable the application and enable the overlay.
  - The `Test flash` button is mainly used for debugging and checking how well the flashbang effect works on your machine. If you are expiriencing issues, please make an [issue](https://github.com/Exilon24/TwitchFlashbang/tree/v0.1.0#issues-bug-reports-and-suggestions).
  - Finally, the app has a flashbang queueing system. Half as to not destroy the machine that the app is running on and half because its way easier to program as a bug failsafe. If an event is triggered whilst the flashbang is active, the queue will increase.
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

# Issues, Bug reports and suggestions
If you find a bug with the app, have a suggestion or just have a general issue with the app, please create a new issue on the [issues section](https://github.com/Exilon24/TwitchFlashbang/issues) with the appropriate tags. They will be reviewed, answered or resolved if they can. Please also provide evidence or examples to any issue or problem you may be having as it makes it infinitely easier to find and fix the issue.

> ### Please avoid posting known issues as they probably will be closed on post. Here are a list of known issues:
> * The flashbang overlay doesn't properly fit the screen
> * The app locked at 60 FPS.
>   - There might be an option to change this later but as of know, the flashbang effect is dependent on the framerate of the overlay. This is how the overlay works and I can't really do much about it **(Note: If the Win32 version is developed, this may not be a limitation)**. If you're streaming 
or recording, 60 FPS should already be fine for your viewers as usually videos and streams aren't recorded at more than 60 FPS. If you're using a 60hz monitor, the flashbang effect may look a little choppy but otherwise, its still fine.
> * The flashbang has an odd cutoff effect at the end.

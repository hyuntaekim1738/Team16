# Team 16 Project - Yellowstone Wildlife Museum
This project is a VR museum containing interactions and information about the history of Yellowstone National Park. It is currently in its prototype stage. All basic features have been implemented, 1 advanced feature implemented, and 2/4 eras created.
## Relevant Scenes
The project is contained in the scenes:
- startScene.unity
- era4present.unity
- era1origin.unity
startScene.unity is the landing scene that contains the title screen. Once the user 
clicks to bypass the screen, it lands on era4present.unity. Users can switch between era4present.unity and era1origin.unity using the time travel feature.
## Setup Guide
Target Device is standard Android/Google Cardboard setup. To use the Ranger AI project requires setting up a connection to Gemini API. If these setup steps are skipped, the VR application will run, and the only feature that won't work is the Gemini API calling, but the application is robust enough to handle the error.
Create your Gemini API Key
- Navigate to https://aistudio.google.com/api-keys
- This might require a login if you are not already logged into Google
- In the top right click "Create API Key"
- The API key requires a project. Click the "Create Project" Option and name it whatever you want. Assign it to the key.
- Copy the API key
Add API information to the server.
- In the Team16 repo, navigate to Team16/yellowstone-ranger-api/.env
- In the line that says "GEMINI_API_KEY=API_KEY_HERE", replace API_KEY_HERE with your API key
- In the commandline, navigate to Team16/yellowstone-ranger-api and run "npm install"
- Once that it is complete, run "node server.js". Keep the server running
Add IP information to the Unity application
- Navigate to Team16/Assets/Scripts/RangerMenuUI.cs
- Line 37 looks like this: private string rangerApiUrl = "http://IP-HERE:3000/ask-ranger";
- Replace IP-HERE with the IP address of the device that is running the server
- Ensure that the Android device and the server running device are on the same internet network.
Once these steps have been completed, rebuild the application, keep the server running, and all features should work normally.
## Github Link
https://github.com/hyuntaekim1738/Team16
## Interaction Techniques
### Signs
The sign feature displays a titled sign that the user can click on to expand for more information. Signs are the brown rectangles placed around the virtual environment.  
To interact with the sign, aim the reticle pointer at the sign and press "B". This will display an expanded sign in front of the user. User movement will be disabled while the sign is expanded. To close the sign, aim the reticle pointer at the close button located on the button of the sign and press "B".
### Park Ranger
The park ranger is the humanoid figure in the park that users can query for more information. The ranger will display information in a panel adjacent to it.
To interact with the ranger, aim the reticle pointer at the ranger object and press "B". This will open a menu with clearly buttons for options for question topics that you can ask the ranger about. User movement will be disabled while the menu is open. To select each menu option, aim the reticle pointer at the desired button and press "B".  
One of the options is "Custom Question". If that option is selected, a button called "Speak Question" will appear. Users can select that button and verbally ask a question. Unity Speech to Text will send the detected question to the Gemini API, return an answer, and display the answer in the answer text panel. This requires microphone permissions.  
To close the menu, simply select the "exit" option.
### Animals
There are animal objects located around the park that the users can interact with.  
To interact with the animals, aim the reticle pointer at the animal and press "B". This will open a panel containing information about the animal. User movement will be disabled while the panel is open. To close the menu, aim the reticle pointer at the close button and press "B".
### Animal Food
There are bins containing animal food (steak and leaf) in the park. This food can be fed to the animals.  
To interact with the food, aim the reticle pointer at the food and press "B". The food will disappear and reappear anchored to the user camera. To feed the animal with this food, aim the reticle pointer at the animal that the food is meant for and press "B". The food will disappear. 
### Binoculars
There is a stand with binoculars mounted on top in the virtual environment. This will let the user see the park at various zoom levels.  
To interact with the binoculars, aim the reticle pointer at the binoculars stand and press "B". This will disable user movement and display a binoculars view with 2 menu buttons: a zoom control button and a close button. The user can navigate between these 2 buttons using the joystick, and the user can toggle between various zoom options by selecting the zoom control button and pressing "B". To close the binoculars view, select the close button and press "B".
### Time Travel
The user can transport to different eras of the park. On the porch of the cabin in the virtual environment, there are circular pads highlighted yellow or orange. The orange pad represents your current era while the yellow pad represents an era you can transport to.  
To transport to a different era, stand on top of a yellow pad. A confirmation message will appear. You can select "Yes" or "No" by using the joystick and pressing "B" on the controller. If "Yes" is selected, the scene representing the era you want to transport to will load.
### Teleportation
The user can teleport around the environment by pointing at the green plane on the ground and pressing "A". The user cannot teleport outside of the fences.
### Menu
Users can open a settings menu by pressing the 3 lined button on the controller. This will open a menu that displays the controls and has various menu options that are clearly labelled. Users can navigate the menu by using the joystick and pressing "B" on the desired options.
## Youtube Link
Here is a link containing a demo to our project!
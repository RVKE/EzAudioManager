HOW TO USE (WIP):

1. Create an empty gameobject in you scene and name it anyway you want
2. Attach the AudioManager component to the object
3. Create a new sound by rightclicking in the project window and going: Create -> EzAudioManager -> Sound
4. Attach your audioclip(s) in the main settings and tweak all settings to your liking
5. Use the green button at the top to attach the audioclip to the AudioManager (you can also manually attach Sounds to the AudioManager)
6. To play your Sound in your script use: AudioManager.Play("Name") The string should correspond to name you've given the sound in the inspector.
7. You can also stop, mute, pause, unpause and stop the sound.
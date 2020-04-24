Version 1.0
---------------------------------------------------------------
FORBIDDEN REDISTRIBUTION & COMMERCIAL USE AVAILABLE
---------------------------------------------------------------
SETUP (Not required in my sample project because it's already set, this is required in your project)

	Put AndroidManifest.xml in ./Assets/Plugins/Android/ like my sample project

	Required Code
		1)
		<service android:name="com.nado.stepcounter.StepCounterService">
            <intent-filter>
                <action android:name="stepcounter.nado.com"/>
                <category android:name="android.intent.category.DEFAULT"/>
            </intent-filter>
    	</service>
    	2)
    	<uses-feature android:name="android.hardware.sensor.stepcounter" android:required="false"/>

	Option (Put this if you want auto-start step counter service when device rebooted)
		<receiver android:name="com.nado.stepcounter.RebootReceiver">  
            <intent-filter>     
                <action android:name="android.intent.action.BOOT_COMPLETED"/>  
                <category android:name="android.intent.category.DEFAULT" />  
            </intent-filter>  
    	</receiver>
---------------------------------------------------------------
There are 3 methods in Step Counter Plugin
	1. int GetStepCount()
		1) Return the number of steps since boot
		2) The number of steps will only be reset when device rebooted
		3) Reference video provided by Google (https://www.youtube.com/watch?v=yv9jskPvLUc)
	2. void StartStepCounterService()
		1) Start step counter service
		2) If you call this function when already service running, nothing happen
	3. void StopStepCounterService()
		1) Stop step counter service
		2) If you call this function when service not running, nothing happen
---------------------------------------------------------------
NOTE
	Not works on Unity Editor

	Works on Android API 19+
	
	Step count will be reset when device rebooted.
	That's because the sensor is built that way.
	If you want to keep the number of steps after rebooting, you have to create your own logic. For example, you can use PlayerPrefs.
---------------------------------------------------------------
Author
	- Nado
	- dev.kwoak@gmail.com
---------------------------------------------------------------







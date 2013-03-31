/**
 * 
 */
package com.mythiksoftware.ProjectFrame;

import com.badlogic.gdx.Gdx;

/**
 * @author James
 *
 */
public class Logger {
	public enum LogLevel{
		All,
		Debug,
		Error
	}
	
	public static LogLevel level = LogLevel.All;
	
	public static void Log(String msg){
		Log(msg,LogLevel.Debug);
	}
	public static void Log(String msg,LogLevel eLevel){
		if(eLevel.compareTo(level) >= 0)
		Gdx.app.log(eLevel.toString(), msg);
	}

}

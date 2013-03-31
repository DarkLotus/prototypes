/**
 * 
 */
package com.mythiksoftware.ProjectFrame;

import java.util.Dictionary;
import java.util.HashMap;
import java.util.Map;

import sun.rmi.runtime.Log;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.files.FileHandle;
import com.badlogic.gdx.graphics.Texture;
import com.badlogic.gdx.graphics.Texture.TextureFilter;
import com.badlogic.gdx.graphics.g2d.Sprite;
import com.badlogic.gdx.graphics.g2d.TextureRegion;
import com.badlogic.gdx.scenes.scene2d.Actor;

/**
 * @author James
 *
 */
public class GraphicsManager {
	private static Texture _tileSetTexture;
	private static Texture _objectsSeTexture;
	private static GraphicsManager _man;
	
	public GraphicsManager() {
		_tileSetTexture = new Texture(Gdx.files.internal("data/tileset.png"));
		_tileSetTexture.setFilter(TextureFilter.Linear, TextureFilter.Linear);
		_objectsSeTexture = new Texture(Gdx.files.internal("data/objects.png"));
		_objectsSeTexture.setFilter(TextureFilter.Linear, TextureFilter.Linear);
		
		//this.ObjectNames = loadObjectNames("objects.data");
	}
private HashMap<String, TextureRegion> loadObjectNames(String string) {
	HashMap<String, TextureRegion> temp = new HashMap<String, TextureRegion>();
	FileHandle fileHandle = Gdx.files.internal("data/"+string);
		String[] objectsStrings = fileHandle.readString().split("[\r\n]");
		for (String string2 : objectsStrings) {
			if(string2.length() < 5)
			continue;

			int x = Integer.parseInt(string2.split(" ")[1]);
			int y = Integer.parseInt(string2.split(" ")[2]);
			int w = Integer.parseInt(string2.split(" ")[3]);
			int h = Integer.parseInt(string2.split(" ")[4]);
			TextureRegion textureRegion = new TextureRegion(_objectsSeTexture, x*32, y*32, w*32, h*32);
			temp.put(string2.split(" ")[0], textureRegion);
		}
		return temp;
	}
public static GraphicsManager getManager() {
	if(_man == null){
		_man = new GraphicsManager();
	}
	return _man;
}
	
	public TextureRegion getTileWithID(int id){
		return LoadTexture(id);
	}
	
	public TextureRegion getSpriteWithID(int id){
		return LoadTexture(id);
	}
	
	private TextureRegion LoadTexture(int id) {
		if(id == 0)
			return null;
		boolean isTile = true;
		if(id > 1025){
			id = id - 1024;
		isTile  = false;
		}
		id--;
		int x = id % 32;
		int y = id / 32;
		if(isTile){
			return getTileWithXY(x, y);
			}
		
		return getSpriteWithXY(x, y);
		
		
	}

	
	public TextureRegion getSpriteWithXY(int x, int y) {
		if(x*64 > _objectsSeTexture.getWidth() || y*64 > _objectsSeTexture.getHeight())
		{
			Gdx.app.log("DEBUG", _objectsSeTexture.getWidth() + " , " + _objectsSeTexture.getHeight());
			return null;
		}
		TextureRegion region = new TextureRegion(_objectsSeTexture, x*64, y*64, 64, 64);
		return region;
		
	}
	public TextureRegion getTileWithXY(int x, int y) {
		TextureRegion region = new TextureRegion(_tileSetTexture, x*64, y*64, 64, 64);
		return region;
		
	}
	
	public HashMap<String, TextureRegion> ObjectNames;
}

/**
 * 
 */
package World;

import java.io.FileInputStream;
import java.io.FileNotFoundException;

import GUI.InGameGUI;
import Managers.PersistenceManager;
import Systems.AISystem;
import Systems.BuildSystem;
import Systems.KeyboardCameraMoveSystem;
import Systems.KeyboardPlayerControllerInputSystem;
import Systems.MapRenderSystem;
import Systems.MouseZoomSystem;
import Systems.MovementSystem;
import Systems.RoomBuildSystem;
import Systems.SpriteRenderSystem;
import Systems.UIRenderSystem;

import com.artemis.Entity;
import com.artemis.managers.GroupManager;
import com.artemis.utils.ImmutableBag;
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.files.FileHandle;
import com.badlogic.gdx.graphics.GL10;
import com.badlogic.gdx.graphics.OrthographicCamera;
import com.badlogic.gdx.math.Vector2;
import com.badlogic.gdx.scenes.scene2d.Actor;
import com.badlogic.gdx.scenes.scene2d.Stage;
import com.badlogic.gdx.scenes.scene2d.utils.ChangeListener;
import com.esotericsoftware.kryo.io.Input;
import com.mythiksoftware.ProjectFrame.EntityFactory;
import com.mythiksoftware.ProjectFrame.Logger;

import components.RoomComponent;
import components.UIButtonComponent;

/**
 * @author James
 *
 */
public class World {
	private com.artemis.World _world;
	OrthographicCamera _camera;
	OrthographicCamera _uiCamera;
	
	private InGameGUI _guiGameGUI;

	public World(){
		set_world(new com.artemis.World());

		
		//_guiGameGUI = new InGameGUI(_world);
		_camera = new OrthographicCamera(Gdx.graphics.getWidth(), Gdx.graphics.getHeight());
		_uiCamera = new OrthographicCamera(Gdx.graphics.getWidth(), Gdx.graphics.getHeight());
		
		//move the ui camera so 0,0 is at the corner, not centre.
		_uiCamera.position.add(Gdx.graphics.getWidth()/2, (Gdx.graphics.getHeight()/2), 0);
		
		//move ingame camera, should be set to centre on map TODO
		_camera.position.add(Gdx.graphics.getWidth()/2, Gdx.graphics.getHeight()/2, 0);
		
		get_world().setSystem(new MapRenderSystem(_camera));
		get_world().setSystem(new SpriteRenderSystem(_camera));
		get_world().setSystem(new MouseZoomSystem(_camera));
		get_world().setSystem(new KeyboardCameraMoveSystem(_camera));
		
		get_world().setSystem(new BuildSystem(_camera));
		//get_world().setSystem(new KeyboardPlayerControllerInputSystem(_camera));//Platformer controller
		get_world().setSystem(new MovementSystem(0.0f)); //todo fix speed?
		
		get_world().setSystem(new UIRenderSystem());
		//_world.setSystem(new AISystem(1));
		get_world().setManager(new GroupManager());
		get_world().initialize();
		
		
		get_world().addEntity(EntityFactory.createMap(get_world(), "world.tmx"));
		get_world().addEntity(EntityFactory.createPlayer(_world, "lotus"));
		//_world.addEntity(EntityFactory.createObject(_world, ""));
		
		get_world().addEntity(EntityFactory.createButton(_world, "Save", null, new ChangeListener(){

			@Override
			public void changed(ChangeEvent event, Actor actor) {
				Logger.Log("clicked save");
				PersistenceManager.PersistToKryo(get_world());
				
			}}));
		get_world().addEntity(EntityFactory.createButton(_world, "add item", null, new ChangeListener(){

			@Override
			public void changed(ChangeEvent event, Actor actor) {
				Logger.Log("clicked add");
				_world.addEntity(EntityFactory.createObject(_world, "test"));
				
			}}));
		
		get_world().addEntity(EntityFactory.createButton(_world, "wipe", null, new ChangeListener(){

			@Override
			public void changed(ChangeEvent event, Actor actor) {
				Logger.Log("clicked wipe");
				ImmutableBag<Entity> entities = _world.getManager(GroupManager.class).getEntities("persist");
				for (int i = entities.size()-1;i >=0;i--) {
					entities.get(i).deleteFromWorld();
				}
				//PersistenceManager.Load(_world, "save.xml");
				
			}}));
		get_world().addEntity(EntityFactory.createButton(_world, "load", null, new ChangeListener(){

			@Override
			public void changed(ChangeEvent event, Actor actor) {
				Logger.Log("clicked load");
				FileHandle f = Gdx.files.external("save.bin");
				try {
					PersistenceManager.LoadFromKryo(_world, new Input(new FileInputStream(f.file())));
				} catch (FileNotFoundException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				}
				
			}}));
		
		get_world().addEntity(EntityFactory.createButton(_world, "build a room", null, new ChangeListener(){

			@Override
			public void changed(ChangeEvent event, Actor actor) {
				Logger.Log("clicked room");
				//TODO check for gold would be in this phase i guess.
				//get_world().getSystem(RoomBuildSystem.class).StartBuild(new RoomComponent());
				get_world().addEntity(EntityFactory.createResidential(_world));
				
			}}));
		
		
	}

	/**
	 * @param delta
	 */
	public void render(float delta) {
		Gdx.gl.glClear(GL10.GL_COLOR_BUFFER_BIT);
		_camera.update();
		//_uiCamera.update();
		
		get_world().setDelta(delta);
		get_world().process();
		//_guiGameGUI.render();
	}

	/**
	 * @return the _world
	 */
	public com.artemis.World get_world() {
		return _world;
	}

	/**
	 * @param _world the _world to set
	 */
	public void set_world(com.artemis.World _world) {
		this._world = _world;
	}

	
}

/**
 * 
 */
package World;

import GUI.InGameGUI;
import GUI.UIRenderSystem;
import Managers.PersistenceManager;
import Systems.AISystem;
import Systems.KeyboardMoveSystem;
import Systems.MapRenderSystem;
import Systems.MouseZoomSystem;
import Systems.SpriteRenderSystem;

import com.artemis.Entity;
import com.artemis.managers.GroupManager;
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.graphics.GL10;
import com.badlogic.gdx.graphics.OrthographicCamera;
import com.badlogic.gdx.math.Vector2;
import com.badlogic.gdx.scenes.scene2d.Actor;
import com.badlogic.gdx.scenes.scene2d.Stage;
import com.badlogic.gdx.scenes.scene2d.utils.ChangeListener;
import com.mythiksoftware.ProjectFrame.EntityFactory;
import com.mythiksoftware.ProjectFrame.Logger;

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
		_uiCamera.position.add(Gdx.graphics.getWidth()/2, (Gdx.graphics.getHeight()/2), 0);
		_camera.position.add(Gdx.graphics.getWidth()/2, Gdx.graphics.getHeight()/2, 0);
		//_stage.getCamera().translate(64*20, 64*10, 0);
		get_world().setSystem(new MapRenderSystem(_camera));
		get_world().setSystem(new SpriteRenderSystem(_camera));
		get_world().setSystem(new MouseZoomSystem(_camera));
		get_world().setSystem(new KeyboardMoveSystem(_camera));
		get_world().setSystem(new UIRenderSystem());
		//_world.setSystem(new AISystem(1));
		get_world().setManager(new GroupManager());
		get_world().initialize();
		
		get_world().addEntity(EntityFactory.createMap(get_world(), "map.tmx"));
		//_world.addEntity(EntityFactory.createObject(_world, ""));
		
		get_world().addEntity(EntityFactory.createButton(_world, "Save", null, new ChangeListener(){

			@Override
			public void changed(ChangeEvent event, Actor actor) {
				Logger.Log("clicked save");
				PersistenceManager.Persist(get_world());
				
			}}));
		get_world().addEntity(EntityFactory.createButton(_world, "add item", null, new ChangeListener(){

			@Override
			public void changed(ChangeEvent event, Actor actor) {
				Logger.Log("clicked add");
				_world.addEntity(EntityFactory.createObject(_world, "test"));
				
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

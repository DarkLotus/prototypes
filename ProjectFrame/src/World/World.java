/**
 * 
 */
package World;

import Systems.AISystem;
import Systems.MapRenderSystem;
import Systems.MouseZoomSystem;
import Systems.SpriteRenderSystem;

import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.graphics.GL10;
import com.badlogic.gdx.graphics.OrthographicCamera;
import com.badlogic.gdx.scenes.scene2d.Stage;
import com.mythiksoftware.ProjectFrame.EntityFactory;

/**
 * @author James
 *
 */
public class World {
	private com.artemis.World _world;
	private Stage _stage;
	
	public World(){
		_world = new com.artemis.World();
		_stage = new Stage();
		_world.setSystem(new MapRenderSystem((OrthographicCamera) _stage.getCamera()));
		_world.setSystem(new SpriteRenderSystem((OrthographicCamera) _stage.getCamera()));
		_world.setSystem(new MouseZoomSystem((OrthographicCamera) _stage.getCamera()));
		_world.setSystem(new AISystem(0.1f));
		_world.initialize();
		
		_world.addEntity(EntityFactory.createMap(_world, "map.tmx"));
		_world.addEntity(EntityFactory.createObject(_world, ""));
	}

	/**
	 * @param delta
	 */
	public void render(float delta) {
		Gdx.gl.glClear(GL10.GL_COLOR_BUFFER_BIT);
		_world.setDelta(delta);
		_world.process();
		_stage.act(delta);
		_stage.draw();
	}

	
}

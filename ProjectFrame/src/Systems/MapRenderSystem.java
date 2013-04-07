/**
 * 
 */
package Systems;

import com.artemis.Aspect;
import com.artemis.ComponentManager;
import com.artemis.ComponentMapper;
import com.artemis.Entity;
import com.artemis.EntitySystem;
import com.artemis.annotations.Mapper;
import com.artemis.systems.EntityProcessingSystem;
import com.artemis.systems.VoidEntitySystem;
import com.artemis.utils.ImmutableBag;
import com.badlogic.gdx.graphics.Camera;
import com.badlogic.gdx.graphics.OrthographicCamera;
import com.badlogic.gdx.maps.tiled.TiledMap;
import com.badlogic.gdx.maps.tiled.TmxMapLoader;
import com.badlogic.gdx.maps.tiled.renderers.OrthogonalTiledMapRenderer;

import components.MapComponent;

/**
 * @author James
 *
 */
public class MapRenderSystem extends EntityProcessingSystem {
	@Mapper
	ComponentMapper<MapComponent> mc;
	
	private OrthogonalTiledMapRenderer _renderer;
	private OrthographicCamera _camera;
	private TiledMap _map;
	/**
	 * @param camera
	 */


	
	public MapRenderSystem(OrthographicCamera camera) {
		super(Aspect.getAspectForAll(MapComponent.class));
		_camera = camera;
		
	}
	/* (non-Javadoc)
	 * @see com.artemis.systems.EntityProcessingSystem#process(com.artemis.Entity)
	 */
	@Override
	protected void process(Entity e) {
		if(mc.has(e))
		{
			
			if(_map == null){
				MapComponent m = mc.getSafe(e);
				_map = new TmxMapLoader().load("data/" + m.MapNameString);
				_renderer = new OrthogonalTiledMapRenderer(_map, 1f);
			}
			_renderer.setView(_camera);
			_renderer.render();
		}
		
	}


	

}

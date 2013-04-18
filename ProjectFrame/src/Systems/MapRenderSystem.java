/**
 * 
 */
package Systems;

import com.artemis.Aspect;
import com.artemis.ComponentMapper;
import com.artemis.Entity;
import com.artemis.annotations.Mapper;
import com.artemis.systems.EntityProcessingSystem;
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
		this._camera = camera;

	}
	/* (non-Javadoc)
	 * @see com.artemis.systems.EntityProcessingSystem#process(com.artemis.Entity)
	 */
	@Override
	protected void process(Entity e) {
		if(this.mc.has(e))
		{

			if(this._map == null){
				MapComponent m = this.mc.getSafe(e);
				this._map = new TmxMapLoader().load("data/" + m.MapNameString);
				this._renderer = new OrthogonalTiledMapRenderer(this._map, 1f);
			}
			this._renderer.setView(this._camera);
			this._renderer.render();
		}

	}




}

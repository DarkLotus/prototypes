/**
 * 
 */
package Systems;

import com.artemis.Aspect;
import com.artemis.ComponentManager;
import com.artemis.ComponentMapper;
import com.artemis.Entity;
import com.artemis.EntitySystem;
import com.artemis.World;
import com.artemis.annotations.Mapper;
import com.artemis.systems.EntityProcessingSystem;
import com.artemis.systems.VoidEntitySystem;
import com.artemis.utils.ImmutableBag;
import com.badlogic.gdx.graphics.Camera;
import com.badlogic.gdx.graphics.OrthographicCamera;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.badlogic.gdx.graphics.g2d.TextureRegion;
import com.badlogic.gdx.maps.tiled.TiledMap;
import com.badlogic.gdx.maps.tiled.TmxMapLoader;
import com.badlogic.gdx.maps.tiled.renderers.OrthogonalTiledMapRenderer;

import com.mythiksoftware.ProjectFrame.GraphicsManager;

import components.MapComponent;
import components.SpriteComponent;
import components.WorldPositionComponent;

/**
 * @author James
 *
 */
public class SpriteRenderSystem extends EntityProcessingSystem {
	@Mapper
	ComponentMapper<WorldPositionComponent> wc;
	
	@Mapper
	ComponentMapper<SpriteComponent> sc;
	

	private OrthographicCamera _camera;
	
	/**
	 * @param camera
	 */


	SpriteBatch batch;
	public SpriteRenderSystem(OrthographicCamera camera) {
		super(Aspect.getAspectForAll(SpriteComponent.class,WorldPositionComponent.class));
		_camera = camera;
		batch = new SpriteBatch();
		
	}
	
	 @Override
     protected void begin() {
             batch.setProjectionMatrix(_camera.combined);
             batch.begin();
     }
	/* (non-Javadoc)
	 * @see com.artemis.systems.EntityProcessingSystem#process(com.artemis.Entity)
	 */
	@Override
	protected void process(Entity e) {
		if(sc.has(e) && wc.has(e))
		{
			TextureRegion textureRegion = null; // todo cache
			SpriteComponent s = sc.getSafe(e);
			WorldPositionComponent w = wc.getSafe(e);
			if(s.SpriteID != 0)
			textureRegion = GraphicsManager.getManager().getSpriteWithID(s.SpriteID);
			else if(s.SpriteName != null)
				textureRegion = GraphicsManager.getManager().ObjectNames.get(s.SpriteName);
			else {
				textureRegion = GraphicsManager.getManager().getSpriteWithXY(0, 0);
			}
			batch.draw(textureRegion, w.x, w.y);
		}
		
		}
	@Override
	protected void end() {
        batch.end();
}

	

}

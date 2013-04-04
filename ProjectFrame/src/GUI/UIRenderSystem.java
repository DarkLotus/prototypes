/**
 * 
 */
package GUI;

import java.util.Dictionary;
import java.util.HashMap;
import java.util.Map;

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
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.graphics.Camera;
import com.badlogic.gdx.graphics.OrthographicCamera;
import com.badlogic.gdx.graphics.Texture.TextureFilter;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.badlogic.gdx.graphics.g2d.TextureRegion;
import com.badlogic.gdx.maps.tiled.TiledMap;
import com.badlogic.gdx.maps.tiled.TmxMapLoader;
import com.badlogic.gdx.maps.tiled.renderers.OrthogonalTiledMapRenderer;
import com.badlogic.gdx.scenes.scene2d.Actor;
import com.badlogic.gdx.scenes.scene2d.ui.Skin;
import com.badlogic.gdx.scenes.scene2d.ui.TextButton;

import com.mythiksoftware.ProjectFrame.GraphicsManager;

import components.MapComponent;

import components.SpriteComponent;
import components.UIButtonComponent;
import components.WorldPositionComponent;

/**
 * @author James
 *
 */
public class UIRenderSystem extends EntityProcessingSystem {

	
	@Mapper
	ComponentMapper<UIButtonComponent> bc;
	

	private OrthographicCamera _camera;
	
	/**
	 * @param camera
	 */


	SpriteBatch batch;


	private Skin _skin;
	public UIRenderSystem(OrthographicCamera camera) {
		super(Aspect.getAspectForOne(UIButtonComponent.class));
		_camera = camera;
		batch = new SpriteBatch();
		_uiElementsCache = new HashMap<Integer,Actor>();
		_skin = new Skin(Gdx.files.internal("data/uiskin.json"));
		_skin.getAtlas().getTextures().iterator().next().setFilter(TextureFilter.Nearest, TextureFilter.Nearest);
		
	}
	
	 @Override
     protected void begin() {
             batch.setProjectionMatrix(_camera.combined);
             batch.begin();
     }
	 
	 
	 HashMap<Integer, Actor> _uiElementsCache;
	/* (non-Javadoc)
	 * @see com.artemis.systems.EntityProcessingSystem#process(com.artemis.Entity)
	 */
	@Override
	protected void process(Entity e) {
		if(bc.has(e))
		{
			UIButtonComponent b = bc.getSafe(e);
			if(!_uiElementsCache.containsKey(e.getId())){
				TextButton button = new TextButton(b.getLabel(), _skin);
				button.setX(b.getLocation().x);
				button.setY(b.getLocation().y);
				if(b.getListener() != null)
				button.addListener(b.getListener());
				_uiElementsCache.put(e.getId(), button);
			}
			_uiElementsCache.get(e.getId()).draw(batch, 1f);
		}
		
		}
	@Override
	protected void end() {
        batch.end();
}

	

}

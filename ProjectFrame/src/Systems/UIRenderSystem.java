/**
 * 
 */
package Systems;

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
import com.badlogic.gdx.scenes.scene2d.Stage;
import com.badlogic.gdx.scenes.scene2d.ui.Skin;
import com.badlogic.gdx.scenes.scene2d.ui.Table;
import com.badlogic.gdx.scenes.scene2d.ui.TextButton;

import com.mythiksoftware.ProjectFrame.GameEngine;
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
	

	Stage _stage;
	Table _table;

	private Skin _skin;
	public UIRenderSystem() {
		super(Aspect.getAspectForOne(UIButtonComponent.class));
		
		_stage = new Stage();
		GameEngine.addInputHandler(_stage);
		_uiElementsCache = new HashMap<Integer,Actor>();
		_skin = new Skin(Gdx.files.internal("data/uiskin.json"));
		_skin.getAtlas().getTextures().iterator().next().setFilter(TextureFilter.Nearest, TextureFilter.Nearest);
		_table = new Table(_skin);
		_table.setFillParent(true);
		_table.top();
		_table.left();
		_stage.addActor(_table);
		// take a stage instead of a camera, handle objects as they are but add to a stage instead.
	}
	
	 @Override
     protected void begin() {
            // batch.setProjectionMatrix(_camera.combined);
            // batch.begin();
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
				TextButton button = new TextButton(b.LabelString, _skin);
				if(b.Location != null){
				button.setX(b.Location.x);
				button.setY(b.Location.y);
				}
				if(b.Listener != null)
				button.addListener(b.Listener);
				_uiElementsCache.put(e.getId(), button);
				_table.add(button);
			}
			//_uiElementsCache.get(e.getId()).draw(batch, 1f);
		}
		
		}
	@Override
	protected void end() {
        _stage.act();
        _stage.draw();
}

	

}

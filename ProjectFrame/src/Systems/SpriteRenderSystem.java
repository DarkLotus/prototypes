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
import com.badlogic.gdx.graphics.g2d.BitmapFont;
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.badlogic.gdx.graphics.g2d.TextureRegion;
import com.mythiksoftware.ProjectFrame.GraphicsManager;

import components.SpriteComponent;
import components.WorldPositionComponent;
import components.city.ResidentialComponent;

/**
 * @author James
 *
 */
public class SpriteRenderSystem extends EntityProcessingSystem {
	@Mapper
	ComponentMapper<WorldPositionComponent> wc;

	@Mapper
	ComponentMapper<SpriteComponent> sc;
	
	@Mapper
	ComponentMapper<ResidentialComponent> rc;


	private OrthographicCamera _camera;
	
	private SpriteBatch _batch;
	private BitmapFont _font;

	/**
	 * @param camera
	 */


	
	public SpriteRenderSystem(OrthographicCamera camera) {
		super(Aspect.getAspectForAll(SpriteComponent.class,WorldPositionComponent.class));
		this._camera = camera;
		this._batch = new SpriteBatch();
		this._font = new BitmapFont();
		this._font.setColor(1f, 1f, 1f, 1f);
	}

	@Override
	protected void begin() {
		this._batch.setProjectionMatrix(this._camera.combined);
		this._batch.begin();
	}
	/* (non-Javadoc)
	 * @see com.artemis.systems.EntityProcessingSystem#process(com.artemis.Entity)
	 */
	@Override
	protected void process(Entity e) {

		
		if(this.sc.has(e) && this.wc.has(e))
		{
			TextureRegion textureRegion = null; // todo cache
			SpriteComponent s = this.sc.get(e);
			WorldPositionComponent w = this.wc.get(e);
			if(s.SpriteID != 0) {
				textureRegion = GraphicsManager.getManager().getSpriteWithID(s.SpriteID);
			} else if(s.SpriteName != null) {
				textureRegion = GraphicsManager.getManager().ObjectNames.get(s.SpriteName);
			} else {
				textureRegion = GraphicsManager.getManager().getSpriteWithXY(0, 0);
			}
			this._batch.draw(textureRegion, w.x, w.y);
		}
		if(this.rc.has(e)){
			ResidentialComponent residentialComponent = rc.get(e);
			WorldPositionComponent w = wc.get(e);
			CharSequence str = residentialComponent.toString();
			this._font.draw(_batch, str, w.x, w.y);
		}
		

	}
	@Override
	protected void end() {
		this._batch.end();
	}



}

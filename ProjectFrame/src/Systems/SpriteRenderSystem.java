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
import com.badlogic.gdx.graphics.g2d.SpriteBatch;
import com.badlogic.gdx.graphics.g2d.TextureRegion;
import com.mythiksoftware.ProjectFrame.GraphicsManager;

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
		this._camera = camera;
		this.batch = new SpriteBatch();

	}

	@Override
	protected void begin() {
		this.batch.setProjectionMatrix(this._camera.combined);
		this.batch.begin();
	}
	/* (non-Javadoc)
	 * @see com.artemis.systems.EntityProcessingSystem#process(com.artemis.Entity)
	 */
	@Override
	protected void process(Entity e) {
		if(this.sc.has(e) && this.wc.has(e))
		{
			TextureRegion textureRegion = null; // todo cache
			SpriteComponent s = this.sc.getSafe(e);
			WorldPositionComponent w = this.wc.getSafe(e);
			if(s.SpriteID != 0) {
				textureRegion = GraphicsManager.getManager().getSpriteWithID(s.SpriteID);
			} else if(s.SpriteName != null) {
				textureRegion = GraphicsManager.getManager().ObjectNames.get(s.SpriteName);
			} else {
				textureRegion = GraphicsManager.getManager().getSpriteWithXY(0, 0);
			}
			this.batch.draw(textureRegion, w.x, w.y);
		}

	}
	@Override
	protected void end() {
		this.batch.end();
	}



}

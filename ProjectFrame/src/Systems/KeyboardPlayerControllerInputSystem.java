/**
 * 
 */
package Systems;

import com.artemis.Aspect;
import com.artemis.ComponentMapper;
import com.artemis.Entity;
import com.artemis.annotations.Mapper;
import com.artemis.systems.EntityProcessingSystem;
import com.badlogic.gdx.Input;
import com.badlogic.gdx.InputProcessor;
import com.badlogic.gdx.graphics.OrthographicCamera;
import com.badlogic.gdx.math.Vector2;
import com.mythiksoftware.ProjectFrame.GameEngine;
import components.PlayerComponent;
import components.VelocityComponent;
import components.WorldPositionComponent;

/**
 * @author James
 *
 */
public class KeyboardPlayerControllerInputSystem extends EntityProcessingSystem implements InputProcessor  {
	@Mapper
	ComponentMapper<VelocityComponent> vc;
	@Mapper
	ComponentMapper<WorldPositionComponent> wc;
	@Mapper
	ComponentMapper<PlayerComponent> pc;

	private OrthographicCamera _camera;

	public KeyboardPlayerControllerInputSystem(OrthographicCamera camera){
		super(Aspect.getAspectForAll(PlayerComponent.class, VelocityComponent.class));
		this._camera = camera;
		GameEngine.addInputHandler(this);
	}

	/* (non-Javadoc)
	 * @see com.artemis.systems.EntityProcessingSystem#process(com.artemis.Entity)
	 */
	@Override
	protected void process(Entity e) {
		// TODO Auto-generated method stub
		VelocityComponent v = this.vc.getSafe(e);
		WorldPositionComponent w = this.wc.getSafe(e);
		this._camera.position.set(w.x, w.y, 0);
		if(!this._usedBoolean){
			v.x = this._camDir.x;
			v.y = this._camDir.y;
			this._usedBoolean = true;
		}


		//Logger.Log(_camera.position.toString() + " player@ " + w.x + " " + w.y);

	}
	Boolean _usedBoolean = false;
	Vector2 _camDir = new Vector2();

	/* (non-Javadoc)
	 * @see com.badlogic.gdx.InputProcessor#keyDown(int)
	 */
	@Override
	public boolean keyDown(int keycode) {
		switch(keycode){
			case Input.Keys.UP:
				this._camDir.add(0, 1);
				this._usedBoolean = false;
				break;
			case Input.Keys.DOWN:
				this._camDir.add(0, -1);this._usedBoolean = false;
				break;
			case Input.Keys.RIGHT:
				this._camDir.add(1, 0);this._usedBoolean = false;
				break;
			case Input.Keys.LEFT:
				this._camDir.add(-1, 0);this._usedBoolean = false;
				break;

		}
		return true;
	}

	/* (non-Javadoc)
	 * @see com.badlogic.gdx.InputProcessor#keyUp(int)
	 */
	@Override
	public boolean keyUp(int keycode) {
		// TODO Auto-generated method stub
		switch(keycode){
			case Input.Keys.UP:
				this._camDir.sub(0, 1);
				break;
			case Input.Keys.DOWN:
				this._camDir.sub(0, -1);
				break;
			case Input.Keys.RIGHT:
				this._camDir.sub(1, 0);
				break;
			case Input.Keys.LEFT:
				this._camDir.sub(-1, 0);
				break;

		}
		this._usedBoolean = false;
		return true;
	}

	/* (non-Javadoc)
	 * @see com.badlogic.gdx.InputProcessor#keyTyped(char)
	 */
	@Override
	public boolean keyTyped(char character) {
		// TODO Auto-generated method stub
		return false;
	}

	/* (non-Javadoc)
	 * @see com.badlogic.gdx.InputProcessor#touchDown(int, int, int, int)
	 */
	@Override
	public boolean touchDown(int screenX, int screenY, int pointer, int button) {
		// TODO Auto-generated method stub
		return false;
	}

	/* (non-Javadoc)
	 * @see com.badlogic.gdx.InputProcessor#touchUp(int, int, int, int)
	 */
	@Override
	public boolean touchUp(int screenX, int screenY, int pointer, int button) {
		// TODO Auto-generated method stub
		return false;
	}

	/* (non-Javadoc)
	 * @see com.badlogic.gdx.InputProcessor#touchDragged(int, int, int)
	 */
	@Override
	public boolean touchDragged(int screenX, int screenY, int pointer) {
		// TODO Auto-generated method stub
		return false;
	}

	/* (non-Javadoc)
	 * @see com.badlogic.gdx.InputProcessor#mouseMoved(int, int)
	 */
	@Override
	public boolean mouseMoved(int screenX, int screenY) {
		// TODO Auto-generated method stub
		return false;
	}

	/* (non-Javadoc)
	 * @see com.badlogic.gdx.InputProcessor#scrolled(int)
	 */
	@Override
	public boolean scrolled(int amount) {

		return false;
	}




}

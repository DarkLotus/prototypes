/**
 * 
 */
package Systems;

import com.artemis.systems.VoidEntitySystem;
import com.badlogic.gdx.Input;
import com.badlogic.gdx.InputProcessor;
import com.badlogic.gdx.graphics.OrthographicCamera;
import com.badlogic.gdx.math.Vector2;
import com.mythiksoftware.ProjectFrame.GameEngine;

/**
 * @author James
 *
 */
public class KeyboardCameraMoveSystem extends VoidEntitySystem implements InputProcessor {

	private OrthographicCamera _camera;

	public KeyboardCameraMoveSystem(OrthographicCamera camera){
		this._camera = camera;
		GameEngine.addInputHandler(this);
		this._camDir.limit(1.4f);
	}


	/* (non-Javadoc)
	 * @see com.artemis.systems.VoidEntitySystem#processSystem()
	 */
	@Override
	protected void processSystem() {
		if(this._camDir.x < this.camSpeed && this._camDir.x > -this.camSpeed) {
			this._camDir.mul(1.1f, 1);
		}
		if(this._camDir.y < this.camSpeed && this._camDir.y > -this.camSpeed) {
			this._camDir.mul(1,1.1f);
		}
		this._camera.translate(this._camDir);

		//_camera.translate(_camDir);
		//_camera.translate(_camDir);
	}
	float camSpeed = 15f;
	Vector2 _camDir = new Vector2();
	/* (non-Javadoc)
	 * @see com.badlogic.gdx.InputProcessor#keyDown(int)
	 */
	@Override
	public boolean keyDown(int keycode) {
		switch(keycode){
			case Input.Keys.UP:
				this._camDir.add(0, 1);
				break;
			case Input.Keys.DOWN:
				this._camDir.add(0, -1);
				break;
			case Input.Keys.RIGHT:
				this._camDir.add(1, 0);
				break;
			case Input.Keys.LEFT:
				this._camDir.add(-1, 0);
				break;

		}
		return false;
	}

	/* (non-Javadoc)
	 * @see com.badlogic.gdx.InputProcessor#keyUp(int)
	 */
	@Override
	public boolean keyUp(int keycode) {
		// TODO Auto-generated method stub
		switch(keycode){
			case Input.Keys.UP:
				this._camDir.mul(1, 0);
				break;
			case Input.Keys.DOWN:
				this._camDir.mul(1, 0);
				break;
			case Input.Keys.RIGHT:
				this._camDir.mul(0, 1);
				break;
			case Input.Keys.LEFT:
				this._camDir.mul(0, 1);
				break;

		}
		return false;
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
		this._camera.zoom += (float)amount /15;
		return false;
	}

}

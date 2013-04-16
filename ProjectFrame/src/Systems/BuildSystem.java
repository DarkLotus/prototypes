/**
 * 
 */
package Systems;

import com.artemis.Aspect;
import com.artemis.ComponentMapper;
import com.artemis.Entity;
import com.artemis.annotations.Mapper;
import com.artemis.systems.EntityProcessingSystem;
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.InputProcessor;
import com.badlogic.gdx.graphics.OrthographicCamera;
import com.badlogic.gdx.math.Intersector;
import com.badlogic.gdx.math.Vector2;
import com.badlogic.gdx.math.Vector3;
import com.badlogic.gdx.math.collision.Ray;
import com.mythiksoftware.ProjectFrame.GameEngine;

import components.OnCursorComponent;
import components.RoomComponent;
import components.VelocityComponent;
import components.WorldPositionComponent;

/**
 * @author James
 * Provides a build system ala sim city. 
 * When an entity has onCursor, it moves the entity till a click places it, should handle checking for collisions
 */
public class BuildSystem extends EntityProcessingSystem implements InputProcessor  {
	Vector3 mouseLocVector2 = new Vector3();
	@Mapper
	ComponentMapper<OnCursorComponent> cc;
	@Mapper
	ComponentMapper<WorldPositionComponent> wc;
	private OrthographicCamera _camera;
	
	//Track current entity being moved?
	private Entity _entity;
	
	private boolean _bCLicked = false;
	private boolean _bBuilding = false;
	
	
	
	/**
	 * @param aspect
	 */
	public BuildSystem(OrthographicCamera camera) {
		super(Aspect.getAspectForAll(OnCursorComponent.class));
		// TODO Auto-generated constructor stub
		_camera = camera;
		GameEngine.addInputHandler(this);
	}

	private float Rnd(float x){
		int xx = (int) (x/64);
		return xx*64;
	}
	/* (non-Javadoc)
	 * @see com.artemis.systems.EntityProcessingSystem#process(com.artemis.Entity)
	 */
	@Override
	protected void process(Entity e) {
		// only called while were building, since no entity will have cursor component unless its being built.
		OnCursorComponent c = cc.getSafe(e);
		if(c == null)
			return;
		WorldPositionComponent w = wc.getSafe(e);
		mouseLocVector2.set(Gdx.input.getX(),Gdx.input.getY(),0);
		_camera.unproject(mouseLocVector2);
		w.x = Rnd(mouseLocVector2.x);
		w.y = Rnd(mouseLocVector2.y);
		_entity = e;
		_bBuilding = true;
		if(_bCLicked)
		{
			_bBuilding = false;
			_bCLicked = false;
			_entity.removeComponent(c);
			
			_entity = null;
			//todo check for allowed placement
			
		}
	}

	/* (non-Javadoc)
	 * @see com.badlogic.gdx.InputProcessor#keyDown(int)
	 */
	@Override
	public boolean keyDown(int keycode) {
		// TODO Auto-generated method stub
		return false;
	}

	/* (non-Javadoc)
	 * @see com.badlogic.gdx.InputProcessor#keyUp(int)
	 */
	@Override
	public boolean keyUp(int keycode) {
		// TODO Auto-generated method stub
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
		if(_bBuilding)
		_bCLicked = true;
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
		// TODO Auto-generated method stub
		return false;
	}

	
	
}

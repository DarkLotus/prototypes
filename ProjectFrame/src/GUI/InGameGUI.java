/**
 * 
 */
package GUI;
import com.artemis.Aspect;
import com.artemis.Entity;
import com.artemis.World;
import com.artemis.systems.EntityProcessingSystem;
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.InputMultiplexer;
import com.badlogic.gdx.graphics.Texture.TextureFilter;
import com.badlogic.gdx.scenes.scene2d.Actor;
import com.badlogic.gdx.scenes.scene2d.Stage;
import com.badlogic.gdx.scenes.scene2d.ui.Skin;
import com.badlogic.gdx.scenes.scene2d.ui.Table;
import com.badlogic.gdx.scenes.scene2d.ui.TextButton;
import com.badlogic.gdx.scenes.scene2d.utils.ChangeListener;
import com.badlogic.gdx.scenes.scene2d.utils.ChangeListener.ChangeEvent;
import com.mythiksoftware.ProjectFrame.EntityFactory;
import com.mythiksoftware.ProjectFrame.GameEngine;
import com.mythiksoftware.ProjectFrame.Logger;



/**
 * @author James
 *
 */
public class InGameGUI{
	private Stage _stage;
	private World _world;
	private Skin _skin;
	
	private Table _uiTopBarTable;
	
	private Table _uiConFirmRoomTable;
	
	public InGameGUI(World world)
	{
		
		_world = world;
		_stage = new Stage();
		GameEngine.addInputHandler(_stage);
		_skin = new Skin(Gdx.files.internal("data/uiskin.json"));
		_skin.getAtlas().getTextures().iterator().next().setFilter(TextureFilter.Nearest, TextureFilter.Nearest);
		createUI();
	}
	
	public void render()
	{
		
		_stage.act();
		_stage.draw();
	}

	/**
	 * 
	 */
	private void createUI() {
		_uiTopBarTable = new Table(_skin);
		_uiTopBarTable.setFillParent(true);
		_uiTopBarTable.top();
		_uiTopBarTable.left();
		_stage.addActor(_uiTopBarTable);
		addButon(_uiTopBarTable,"BuildMode", buildButton);
		addButon(_uiTopBarTable,"BuildMode", buildButton);
		
		
	}

	/**
	 * @param Table
	 * @param Button Label
	 * @param Listener
	 */
	private void addButon(Table table, String label, ChangeListener Listener) {
		TextButton button = new TextButton(label, _skin);
		button.addListener(Listener);
		table.add(button);	
	}
	
	

	
	ChangeListener buildButton = new ChangeListener() {			
		@Override
		public void changed(ChangeEvent event, Actor actor) {
			Logger.Log("clicked");
			
			_world.addEntity(EntityFactory.createObject(_world, "test"));
		}
	};

	
	
}

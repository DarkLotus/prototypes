/**
 * 
 */
package GUI;
import com.artemis.World;
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.graphics.Texture.TextureFilter;
import com.badlogic.gdx.scenes.scene2d.Actor;
import com.badlogic.gdx.scenes.scene2d.Stage;
import com.badlogic.gdx.scenes.scene2d.ui.Skin;
import com.badlogic.gdx.scenes.scene2d.ui.Table;
import com.badlogic.gdx.scenes.scene2d.ui.TextButton;
import com.badlogic.gdx.scenes.scene2d.utils.ChangeListener;
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

		this._world = world;
		this._stage = new Stage();
		GameEngine.addInputHandler(this._stage);
		this._skin = new Skin(Gdx.files.internal("data/uiskin.json"));
		this._skin.getAtlas().getTextures().iterator().next().setFilter(TextureFilter.Nearest, TextureFilter.Nearest);
		createUI();
	}

	public void render()
	{

		this._stage.act();
		this._stage.draw();
	}

	/**
	 * 
	 */
	private void createUI() {
		this._uiTopBarTable = new Table(this._skin);
		this._uiTopBarTable.setFillParent(true);
		this._uiTopBarTable.top();
		this._uiTopBarTable.left();
		this._stage.addActor(this._uiTopBarTable);
		addButon(this._uiTopBarTable,"BuildMode", this.buildButton);
		addButon(this._uiTopBarTable,"BuildMode", this.buildButton);


	}

	/**
	 * @param Table
	 * @param Button Label
	 * @param Listener
	 */
	private void addButon(Table table, String label, ChangeListener Listener) {
		TextButton button = new TextButton(label, this._skin);
		button.addListener(Listener);
		table.add(button);
	}




	ChangeListener buildButton = new ChangeListener() {
		@Override
		public void changed(ChangeEvent event, Actor actor) {
			Logger.Log("clicked");

			//InGameGUI.this._world.addEntity(EntityFactory.createObject(InGameGUI.this._world, "test"));
		}
	};



}

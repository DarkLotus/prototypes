/**
 * 
 */
package GUI;

import com.badlogic.gdx.scenes.scene2d.Stage;
import com.badlogic.gdx.scenes.scene2d.ui.Skin;
import com.badlogic.gdx.scenes.scene2d.ui.Table;
import com.badlogic.gdx.scenes.scene2d.ui.TextButton;
import com.badlogic.gdx.scenes.scene2d.utils.ChangeListener;

/**
 * @author James
 *
 */
public class ConfirmRoomPlacement extends Table {

	Skin _skinSkin;
	/**
	 * 
	 */
	public ConfirmRoomPlacement(Skin skin) {
		super(skin);
		this._skinSkin = skin;
		// TODO Auto-generated constructor stub
	}


	/**
	 * @param Table
	 * @param Button Label
	 * @param Listener
	 */
	private void addButon(Table table, String label, ChangeListener buildButton2) {
		TextButton button = new TextButton(label, this._skinSkin);
		button.addListener(buildButton2);
		table.add(button);
		Stage s = new Stage();
		s.addActor(button);
	}

}

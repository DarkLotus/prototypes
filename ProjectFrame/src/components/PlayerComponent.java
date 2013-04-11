/**
 * 
 */
package components;

import com.artemis.Component;

/**
 * @author James
 *
 */
public class PlayerComponent extends Component {

	/**
	 * @param i
	 * @param j
	 * @param k
	 */
	public PlayerComponent(int i, int j, int k) {
		Score = i;
		Life = j;
		Money = k;
	}
	public int Score;
	public int Life;
	public int Money;
	
	public PlayerComponent()
	{}
}

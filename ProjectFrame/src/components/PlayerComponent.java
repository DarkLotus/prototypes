/**
 * 
 */
package components;

import com.artemis.Component;

/**
 * @author James
 * Stores player data, Score,Lives,Money etc
 */
public class PlayerComponent extends Component {

	/**
	 * @param i
	 * @param j
	 * @param k
	 */
	public PlayerComponent(int i, int j, int k) {
		this.Score = i;
		this.Life = j;
		this.Money = k;
	}
	public int Score;
	public int Life;
	public int Money;

	public PlayerComponent()
	{}
}

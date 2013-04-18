/**
 * 
 */
package com.mythiksoftware.ProjectFrame;

import com.artemis.Entity;
import com.artemis.World;
import com.artemis.managers.GroupManager;
import com.badlogic.gdx.math.Vector2;
import com.badlogic.gdx.scenes.scene2d.utils.ChangeListener;

import components.MapComponent;
import components.OnCursorComponent;
import components.PlayerComponent;
import components.SpriteComponent;
import components.UIButtonComponent;
import components.WorldPositionComponent;
import components.city.ComercialComponent;
import components.city.IndustrialComponent;
import components.city.PowerDeltaComponent;
import components.city.ResidentialComponent;

/**
 * @author James
 *
 */
public class EntityFactory {



	public static Entity createMap(World world, String name)
	{
		Entity entity = world.createEntity();
		entity.addComponent(new MapComponent(name));
		return entity;
	}
	public static Entity createButton(World world, String label,Vector2 loc, ChangeListener handler)
	{
		Entity entity = world.createEntity();
		entity.addComponent(new UIButtonComponent(label,loc,handler));
		return entity;
	}
	public static Entity createObject(World world, String name)
	{
		Entity entity = world.createEntity();
		entity.addComponent(new WorldPositionComponent(5*64,5*64));
		entity.addComponent(new SpriteComponent(5));
		world.getManager(GroupManager.class).add(entity, "persist");
		return entity;
	}
	public static Entity createPlayer(World world, String name)
	{
		Entity entity = world.createEntity();
		//entity.addComponent(new WorldPositionComponent(5*64,5*64));
		//entity.addComponent(new VelocityComponent());
		entity.addComponent(new PlayerComponent(0,0,10000));
		//entity.addComponent(new SpriteComponent(5));
		world.getManager(GroupManager.class).add(entity, "persist");
		return entity;
	}
	/**
	 * @param _world
	 * @return
	 */
	public static Entity createMovable(World world) {
		Entity entity = world.createEntity();
		//entity.addComponent(new WorldPositionComponent(5*64,5*64));
		//entity.addComponent(new VelocityComponent());
		entity.addComponent(new WorldPositionComponent());
		entity.addComponent(new OnCursorComponent());
		entity.addComponent(new SpriteComponent("Residential"));
		//world.getManager(GroupManager.class).add(entity, "persist");
		return entity;
	}

	public static Entity createResidential(World world) {
		Entity entity = world.createEntity();
		//entity.addComponent(new WorldPositionComponent(5*64,5*64));
		//entity.addComponent(new VelocityComponent());
		entity.addComponent(new WorldPositionComponent());
		entity.addComponent(new OnCursorComponent());
		entity.addComponent(new SpriteComponent("Residential"));
		entity.addComponent(new ResidentialComponent());
		entity.addComponent(new PowerDeltaComponent());
		//world.getManager(GroupManager.class).add(entity, "persist");
		return entity;
	}

	public static Entity createComercial(World world) {
		Entity entity = world.createEntity();
		entity.addComponent(new WorldPositionComponent());
		entity.addComponent(new OnCursorComponent());
		entity.addComponent(new SpriteComponent("Commercial"));
		entity.addComponent(new ComercialComponent());
		entity.addComponent(new PowerDeltaComponent());
		//world.getManager(GroupManager.class).add(entity, "persist");
		return entity;
	}
	public static Entity createIndustrial(World world) {
		Entity entity = world.createEntity();
		entity.addComponent(new WorldPositionComponent());
		entity.addComponent(new OnCursorComponent());
		entity.addComponent(new SpriteComponent("Industrial"));
		entity.addComponent(new IndustrialComponent());
		entity.addComponent(new PowerDeltaComponent());
		//world.getManager(GroupManager.class).add(entity, "persist");
		return entity;
	}
	public static Entity createPowerPlant(World world) {
		Entity entity = world.createEntity();
		entity.addComponent(new WorldPositionComponent());
		entity.addComponent(new OnCursorComponent());
		entity.addComponent(new SpriteComponent("Power Plant"));
		//entity.addComponent(new IndustrialComponent());
		entity.addComponent(new PowerDeltaComponent());
		//world.getManager(GroupManager.class).add(entity, "persist");
		return entity;
	}

}

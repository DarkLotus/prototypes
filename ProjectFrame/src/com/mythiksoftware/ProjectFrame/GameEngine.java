package com.mythiksoftware.ProjectFrame;

import Screens.InGameScreen;

import com.badlogic.gdx.Game;
import com.badlogic.gdx.Gdx;
import com.badlogic.gdx.InputMultiplexer;
import com.badlogic.gdx.InputProcessor;

public class GameEngine extends Game {

	GameEngine StaticRefrence;

	@Override
	public void create() {
		float w = Gdx.graphics.getWidth();
		float h = Gdx.graphics.getHeight();

		//inputManager = new InputManager();
		//Gdx.input.setInputProcessor(inputManager);
		setScreen(new InGameScreen());
		if(this.StaticRefrence == null) {
			this.StaticRefrence = this;
		}

	}

	public static void addInputHandler(InputProcessor inputProcessor){
		if(Gdx.input.getInputProcessor() != null)
		{
			InputMultiplexer multiplexer = (InputMultiplexer) Gdx.input.getInputProcessor();
			multiplexer.addProcessor(inputProcessor);
		} else {
			Gdx.input.setInputProcessor(new InputMultiplexer(inputProcessor));
		}
	}

	@Override
	public void dispose() {

	}



	@Override
	public void resize(int width, int height) {
	}

	@Override
	public void pause() {
	}

	@Override
	public void resume() {
	}
}

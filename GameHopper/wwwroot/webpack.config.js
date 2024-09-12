// webpack.config.js

const path = require('path');

module.exports = {
  // Entry point of your application

 // Mode configuration
 mode: 'development',

  entry: path.resolve(__dirname, 'src/index.js'),

  
  // Output configuration
  output: {
    path: path.resolve(__dirname, 'dist'),
    filename: 'bundle.js',
    publicPath: '/dist/',
  },
  
  // Module and rules configuration
  module: {
    rules: [
      {
        test: /\.(js|jsx)$/, // For both .js and .jsx files
        exclude: /node_modules/,
        use: {
          loader: 'babel-loader',
          options: {
            presets: ['@babel/preset-env', '@babel/preset-react'] // Add '@babel/preset-react'
          }
        },
      },
      {
        test: /\.css$/, // For CSS files
        use: ['style-loader', 'css-loader'],
      },
      {
        test: /\.(png|jpg|gif|svg)$/, // For image files
        use: ['file-loader'],
      },
    ],
  },
  
  // Resolve extensions
  resolve: {
    extensions: ['.js', '.jsx'],
  },
  
  // Development server configuration
  devServer: {
    static: {
      directory: path.resolve(__dirname, 'wwwroot'),
    },
    compress: true,
    port: 3000, 
    hot: true,
    open: true,
    proxy: [
      {
        context: ['/'],
        target: 'http://localhost:5294',
        changeOrigin: true,
        secure: false,
        historyApiFallback: true,
      }
    ],
    
    watchFiles: ['src/**/*', 'wwwroot/**/*'],
  },
};

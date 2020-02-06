# SecondMakingAction
VR班ハッカソン用リポジトリ

## キャラクターのシンプルな移動を実装（1/14）
 平面的なキャラクターの移動と走るアニメーションを実装。また、Chinemacineを導入して、カメラが壁を突き抜けないように実装。

## 敵の移動を実装（1/24）
　プレイヤーを追跡してくる敵の移動をナビゲーションを使って実装。一定の高さ以上の壁がある場合は、低い所を迂回してくる。
 
## 攻撃モーションアニメーションを実装（1/26）
 クリックで攻撃のアニメーションが再生される。右クリックで一部コンボが変わるように実装。

## プレイヤーからの攻撃の当たり判定および（2/6）
 アニメーションイベントを設定し、コライダーの有効・無効を切り替え。ナビゲーションで追跡してくる敵に体力（HP）を設定し、ヒットしたらダメージ処理をするように実装。
 
 キャラクターコントローラを使用した移動では、敵に乗り上げてしまうことが分かったので、Rigidbodyを使用した移動に変えた。

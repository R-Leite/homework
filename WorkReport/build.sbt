import com.typesafe.config._
import com.typesafe.sbt.packager.Keys.scriptClasspath

scriptClasspath := {
    val originalClasspath = scriptClasspath.value
    val manifest = new java.util.jar.Manifest()
    manifest.getMainAttributes().putValue("Class-Path", originalClasspath.mkString(" "))
    val classpathJar = (target in Universal).value / "lib" / "classpath.jar"
    IO.jar(Seq.empty, classpathJar, manifest)
    Seq(classpathJar.getName)
}
mappings in Universal += (((target in Universal).value / "lib" / "classpath.jar") -> "lib/classpath.jar")

mappings in Universal ++= {
    val templateDirectory = baseDirectory(_ / "temporary_files").value
    val templateDirectoryLen = templateDirectory.getCanonicalPath.length
    (templateDirectory ** "*").get.map { f: File =>
        f -> ("temporary_files/" + f.getCanonicalPath.substring(templateDirectoryLen))
    }
}

mappings in Universal ++= {
    val templateDirectory = baseDirectory(_ / "xls_template").value
    val templateDirectoryLen = templateDirectory.getCanonicalPath.length
    (templateDirectory ** "*").get.map { f: File =>
        f -> ("xls_template/" + f.getCanonicalPath.substring(templateDirectoryLen))
    }
}

val conf = ConfigFactory.parseFile(new File("conf/application.conf")).resolve()

version := conf.getString("app.version")
name := """work_report"""

resolvers += "Typesafe repository" at "http://repo.typesafe.com/typesafe/releases/"
resolvers += "Typesafe repository mwn" at "http://repo.typesafe.com/typesafe/maven-releases/"

lazy val root = (project in file(".")).enablePlugins(PlayScala)
scalaVersion := "2.11.8"
libraryDependencies ++= Seq(
  jdbc,
  cache,
  ws,
  evolutions,
  "org.webjars" %% "webjars-play" % "2.4.+",
  //"org.webjars" % "bootstrap" % "3.3.+",
  "org.webjars" % "bootstrap" % "3.3.7-1",
  //"org.webjars" % "jquery" % "3.1.+",
  "org.webjars" % "jquery" % "3.2.1",
  "org.webjars" % "jquery-ui" % "1.12.1",
  "org.webjars" % "bootstrap-datepicker" % "1.6.4",
  "org.webjars" % "datatables" % "1.10.13",
  "org.webjars.npm" % "bootstrap-toggle" % "2.2.2",
  "org.webjars.npm" % "chart.js" % "2.5.0",
  "mysql" % "mysql-connector-java" % "5.1.+",
  "com.typesafe.play" %% "anorm" % "2.5.+",
  // https://mvnrepository.com/artifact/org.webjars/bootstrap
  "com.adrianhurt" %% "play-bootstrap" % "1.1-P25-B3",
  "org.scalatestplus.play" %% "scalatestplus-play" % "1.5.1" % Test,
  // https://mvnrepository.com/artifact/com.typesafe.play/play-json_2.11
  "com.typesafe.play" % "play-json_2.11" % "2.5.10",
  "org.apache.poi" % "poi" % "3.+",
  // https://mvnrepository.com/artifact/org.apache.poi/poi-ooxml
  "org.apache.poi" % "poi-ooxml" % "3.+",
  // https://mvnrepository.com/artifact/com.ibm.icu/icu4j
  "com.ibm.icu" % "icu4j" % "58.2",
  // メール送信用
  "com.typesafe.play" %% "play-mailer" % "6.0.1",
  "com.typesafe.play" %% "play-mailer-guice" % "6.0.1",
  // React.js
  "org.webjars" % "react" % "15.3.2",
  "org.webjars" % "superagent" % "1.4.0",
  // react-bootstrap
  "org.webjars.bower" % "react-bootstrap" % "0.30.3",
  "org.webjars" % "bootstrap-datetimepicker" % "2.4.2",
  "org.webjars.npm" % "react-redux" % "5.0.5"
)

// Use LESS
includeFilter in (Assets, LessKeys.less) := "*.less"
